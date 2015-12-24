using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace QuadroTest1
{
    public class Kommunikator
    {
        public class armSetting:ICloneable{
            public byte code { set; get; } public int value { set; get; }
            public object Clone() { return new armSetting( this ); }
            public armSetting(armSetting toCopy)
            {
                this.code = toCopy.code; this.value = toCopy.value;
            }
            public armSetting() { }
            };
        public class armRequestValue{public byte code{set;get;} public bool started{set;get;} public bool stopped{set;get;}};
        public enum kommunikatorStateTyp { connected, disconnected, connecting, error };

        private static int sMaxWaitConfirm = 40;
        private static int sMaxWaitPing = 40;

        public kommunikatorStateTyp mKommunikatorState{ get; private set;}

        private SerialPort mSerialPort;
        private bool requestConnect;
        private bool requestReset;
        private bool requestDisconnect;

        private bool reading = false;
        private bool readingFirst = false;
        private int remainToRead = 0;
        
        private string readingString = String.Empty;
        private int waitedForConfirm = 0;
        private int waitedForPing = 0;
        private Queue<byte> toCommand;
        private List<armRequestValue> toRequest;
        private Queue<armSetting> toSet;
        public Queue<armSetting> recData;
        private Queue<byte> recError;
        private ConcurrentQueue<string> allrecData;

        public int cntRec = 0;
        public int cntSend = 0;
        public int cntPing = 0;


        public Kommunikator()
        {
            resetAll();
        }

        private void resetAll()
        {
            mKommunikatorState = kommunikatorStateTyp.disconnected;
            allrecData = new ConcurrentQueue<string>();
            recData = new Queue<armSetting>();
            toRequest = new List<armRequestValue>();
            toSet = new Queue<armSetting>();
            cntPing = 0;
            cntRec = 0;
            cntSend = 0;
            waitedForConfirm = 0;
            waitedForPing = 0;
            remainToRead = 0;
            reading = false;
            readingFirst = false;
        }

        public void open(String pportName, int pbaudRate)
        {
            try
            {
                mSerialPort = new SerialPort(pportName, pbaudRate);
                mSerialPort.Open();
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(workReviced);
                mSerialPort.Encoding = Encoding.ASCII;
                Debug.WriteLine("Opened Com Port");

            }
            catch
            {
                Debug.WriteLine("Failed to Open Port on Kom Konstructor, portname:" + pportName);
                this.mKommunikatorState = kommunikatorStateTyp.error;
            }
            requestConnect = true;
            stopRequestValue(0); // RESET REQUEST VEKTOR
        }

        public void close()
        {
            if (mSerialPort != null)
            {
                if (mSerialPort.IsOpen)
                {
                    mSerialPort.Close();
                }
                mSerialPort.Dispose();
            }
            resetAll();
        }

        private void workReviced(object sender, SerialDataReceivedEventArgs e)
        {
            if (!mSerialPort.IsOpen)
                return;
            int toRead = mSerialPort.BytesToRead;
            byte[] buf = new byte[toRead];
            mSerialPort.Read(buf, 0, toRead);
            foreach (byte currentChar in buf)
            {
                //Debug.WriteLine((byte)currentChar);
                if (!reading)
                {
                    if (currentChar == 0x02)
                    {
                        readingString = string.Empty;
                        reading = true;
                        readingFirst = true;
                    }
                }
                else
                {
                    if (readingFirst)
                    {
                        remainToRead = 0;
                        switch (currentChar)
                        {
                            case 118:
                                remainToRead = 3;
                            break;
                            case 119:
                            remainToRead = 5;
                            break;
                        }
                        readingFirst = false;
                        readingString += (char)(currentChar);
                    }
                    else if (currentChar == 0x03 && remainToRead <= 0)
                    {
                        if (readingString != string.Empty)
                        {
                            
                            allrecData.Enqueue(readingString);
                        }
                        reading = false;
                    }
                    else
                    {
                        readingString += (char)(currentChar);
                        remainToRead--;
                    }
                }
            }
        }

        private void analyseRec(string ptoAnalyse)
        {
            if(ptoAnalyse==null || ptoAnalyse==string.Empty)
                return;
            char command = ptoAnalyse[0];
            cntRec++;
            switch (command)
            {
                case 'a':
                    waitedForPing = 0;
                    cntPing++;
                    break;
                case 'c':
                    waitedForConfirm = 0;
                    break;
                case 'f':
                    break;
                case 'v':
                    byte[] bufferV = {(byte)ptoAnalyse[3],(byte)ptoAnalyse[2]};
                    short highValV = BitConverter.ToInt16(bufferV, 0);
                    recData.Enqueue(new armSetting { code = (byte)ptoAnalyse[1], value = highValV });
                    if (recData.Count > 500)
                    {
                        recData.Dequeue();
                    }
                    break;
                case 'w':
                    byte[] bufferW = {(byte)ptoAnalyse[5],(byte)ptoAnalyse[4],(byte)ptoAnalyse[3],(byte)ptoAnalyse[2]};
                    int highValW = BitConverter.ToInt32(bufferW, 0);
                    recData.Enqueue(new armSetting { code = (byte)ptoAnalyse[1], value = highValW});
                    if (recData.Count > 500)
                    {
                        recData.Dequeue();
                    }
                    break;
                default:
                    Debug.WriteLine("Dropped Message Coded with: " + ptoAnalyse);
                    break;
            }
        }

        public void queRequestValue(byte pcode, bool pstart, bool pstop)
        {
            if (pstart == pstop)
                return;
            if (!toRequest.Select<armRequestValue,byte>(x => x.code).Contains(pcode))
            {
                toRequest.Add(new armRequestValue { code = pcode, started = !pstart, stopped = !pstop});
            }
        }

        private void workRequestValueQue()
        {
            foreach (byte rcode in toRequest.Where(x => x.started == false).Select<armRequestValue, byte>(x => x.code))
            {
                requestValue(rcode);
            }
            foreach (byte rcode in toRequest.Where(x => x.stopped == false).Select<armRequestValue, byte>(x => x.code))
            {
                stopRequestValue(rcode);
            }
            toRequest.ForEach(x => { x.stopped = true; x.started = true; });
        }

        private void requestValue(byte code)
        {
            byte[] temp = new byte[]{(byte)(((byte)'0')+code),0};
            directSend("r" + BitConverter.ToChar(temp,0));
        }
        
        public void stopRequestValue(byte code)
        {
            directSend("s" + BitConverter.ToChar(new byte[] { 0, (byte)(((byte)'0') + code) }, 0));
        }

        public void sendSetting(byte pCode, int pValue)
        {
            toSet.Enqueue(new armSetting { code = pCode, value = pValue });
        }

        private void workToSet()
        {
            while (toSet.Count > 0)
            {
                armSetting currentToSet = toSet.Dequeue();
                //byte[] temp = new byte[] { (byte)(((byte)'0') + currentToSet.code), 0 };
                //byte[] tempVal = new byte[] { (byte)(currentToSet.value>>8) , (byte)(currentToSet.value) };
                helperBuildCommand('x', currentToSet.code, currentToSet.value, 2);
            }
        }

        public int startConnect()
        {
            if (mKommunikatorState == kommunikatorStateTyp.disconnected)
            {
                return directSend("b");    //request connection
            }
            else
            {
                return 0x01;
            }
        }

        private int directSend(String pToSend)
        {
            int errorState = 0;
            if (pToSend == null || pToSend == String.Empty)
            {
                errorState = 0x02;
            }
            else
            {
                if (mKommunikatorState!=kommunikatorStateTyp.error)
                {
                    if (mSerialPort.IsOpen)
                    {
                        String buildingString = (char)0x02 + pToSend + (char)0x03;
                        mSerialPort.Write(buildingString);
                        cntSend++;
                    }
                    else
                    {
                        errorState = 0x05;
                    }
                }
                else
                {
                    errorState = 0x01;
                }
            }
            return errorState;
        }

        private int directSend(byte[] pToSend, int lenght)
        {
            int errorState = 0;
            if (pToSend == null || lenght <= 0)
            {
                errorState = 0x02;
            }
            else
            {
                if (mKommunikatorState != kommunikatorStateTyp.error)
                {
                    if (mSerialPort.IsOpen)
                    {
                        byte[] buildingArray = new byte[lenght + 2];
                        buildingArray[0] = 0x02;
                        for (int i = 1; i < lenght + 1; i++)
                        {
                            buildingArray[i] = pToSend[i - 1];
                        }
                        buildingArray[lenght + 1] = 0x03;
                        mSerialPort.Write(buildingArray,0,lenght+2);
                        cntSend++;
                    }
                    else
                    {
                        errorState = 0x05;
                    }
                }
                else
                {
                    errorState = 0x01;
                }
            }
            return errorState;
        }

        private int helperBuildCommand(char commandName, byte code, int val, int byteNumber)
        {
            byte[] temp = new byte[byteNumber+2];
            temp[0] = (byte)commandName;
            temp[1] = (byte)(code + ((byte)'0'));
            for (int i = 0; i < byteNumber; i++)
            {
                temp[-i + 2 + byteNumber-1] = (byte)(val >> (8 * i));
            }
            return directSend(temp, byteNumber + 2);
        }

        public void zyklischMain()
        {
            switch (mKommunikatorState)
            {
                case kommunikatorStateTyp.connected:
                    waitedForPing++;
                    directSend("a");
                    while (allrecData.Count > 0)
                    {
                        String toAnalyse;
                        if (allrecData.TryDequeue(out toAnalyse))
                            analyseRec(toAnalyse);
                    }
                    workRequestValueQue();
                    workToSet();
                    if (waitedForPing > sMaxWaitPing)
                    {
                        mKommunikatorState = kommunikatorStateTyp.disconnected;
                        Debug.WriteLine("Disconnected because didnt get ping");
                    }
                    break;
                case kommunikatorStateTyp.connecting:
                    
                    while (allrecData.Count > 0)
                    {
                        String toAnalyse;
                        if (allrecData.TryDequeue(out toAnalyse))
                            analyseRec(toAnalyse);
                    }
                    if (waitedForConfirm > sMaxWaitConfirm)
                    {
                        mKommunikatorState = kommunikatorStateTyp.disconnected;
                        Debug.WriteLine("Didnt connect because didnt get confirm");
                    }
                    else if (waitedForConfirm == 0)
                    {
                        mKommunikatorState = kommunikatorStateTyp.connected;
                        Debug.WriteLine("Connected!");
                        directSend("q");
                    }
                    waitedForConfirm++;
                    break;
                case kommunikatorStateTyp.disconnected:
                    if (requestConnect)
                    {
                        int res = startConnect();
                        if (res == 0)
                        {
                            mKommunikatorState = kommunikatorStateTyp.connecting;
                            waitedForConfirm = 0;
                        }
                        else
                        {
                            mKommunikatorState = kommunikatorStateTyp.error;
                            Debug.WriteLine("Failed to Start Connecting!: error: " + res);
                        }
                        requestConnect = false;
                    }
                    break;
                case kommunikatorStateTyp.error:
                    break;
                default:
                    Debug.WriteLine("Fehler Zyklisch unbekannter Zustand");
                    break;

            }
        }
    
    }
}
