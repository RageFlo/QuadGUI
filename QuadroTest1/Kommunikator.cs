﻿using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadroTest1
{
    class Kommunikator
    {
        public class armSetting{public byte code{set;get;} public int value{set;get;}};
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
        private Queue<byte> toRequest;
        private Queue<armSetting> toSet;
        public Queue<armSetting> recData;
        private Queue<byte> recError;
        private Queue<string> allrecData;

        public int cntRec = 0;
        public int cntSend = 0;
        public int cntping = 0;


        public Kommunikator(String pportName, int pbaudRate)
        {
            mKommunikatorState = kommunikatorStateTyp.disconnected;
            allrecData = new Queue<string>();
            recData = new Queue<armSetting>();
            try
            {
                mSerialPort = new SerialPort(pportName, pbaudRate);
                mSerialPort.Open();
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(workReviced);
                mSerialPort.Encoding = Encoding.ASCII;
                Debug.WriteLine("Opened Com Port");
                
            }
            catch {
                Debug.WriteLine("Failed to Open Port on Kom Konstructor, portname:" + pportName);
                this.mKommunikatorState = kommunikatorStateTyp.error;
            }
            requestConnect = true;
            
        }

        private void workReviced(object sender, SerialDataReceivedEventArgs e)
        {
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
                    cntping++;
                    break;
                case 'c':
                    waitedForConfirm = 0;
                    break;
                case 'f':
                    break;
                case 'v':
                    byte[] buffer = {(byte)ptoAnalyse[3],(byte)ptoAnalyse[2]};
                    int highVal = (byte)ptoAnalyse[2];
                    if (highVal <= 127)
                        highVal = highVal * 256 + (byte)ptoAnalyse[3];
                    else
                        highVal = (255 - highVal +1) * -256 - (255 - (byte)ptoAnalyse[3]);
                    highVal = BitConverter.ToInt16(buffer, 0);
                    recData.Enqueue(new armSetting { code = (byte)ptoAnalyse[1], value = highVal });
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

        public int directSend(String pToSend)
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
        

        public void zyklischMain()
        {
            switch (mKommunikatorState)
            {
                case kommunikatorStateTyp.connected:
                    waitedForPing++;
                    directSend("a");
                    while (allrecData.Count > 0)
                    {
                        analyseRec(allrecData.Dequeue());
                    }
                    if (waitedForPing > sMaxWaitPing)
                    {
                        mKommunikatorState = kommunikatorStateTyp.disconnected;
                        Debug.WriteLine("Disconnected because didnt get ping");
                    }
                    break;
                case kommunikatorStateTyp.connecting:
                    
                    while (allrecData.Count > 0)
                    {
                        analyseRec(allrecData.Dequeue());
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