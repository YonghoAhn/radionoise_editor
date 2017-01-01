﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SocketIOClient;
using Microsoft.Win32;
using Quobject.SocketIoClientDotNet.Client;

namespace Code_Editor
{
    
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public SaveFileDialog sd = new SaveFileDialog();
        Socket socket;   
        public MainWindow()
        {
            InitializeComponent();
            sd.Filter = "C# File|*.cs|Python File|*.py|HTML File|*.html|CSS File|*.css|JS File|*.js|C File|*.c|C++ File|*.cpp|Header File|*.h|Text File|*.txt";
            sd.Title = "저장";
            socket = IO.Socket("http://iwin247.net:8080");
            socket.Connect();
            Console.WriteLine("Socket Connected");
            socket.On("message", (data) =>
            {
                socket.Emit(SendTXT.Text);
                Console.WriteLine(SendTXT.Text);
            });
            Console.WriteLine("Message Send");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Code_Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string Text = new TextRange(CodeEdtior.Document.ContentStart, CodeEdtior.Document.ContentEnd).Text;
            //MessageBox.Show(Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.Key == Key.LeftShift || e.Key == Key.RightShift) && (e.Key == Key.Enter))

            {
                SendTXT.Text += "\r\n";
            }
            else if(e.Key == Key.Enter)
            {
                socket.On("message", (data) =>
                {
                    socket.Emit(SendTXT.Text);
                    Console.WriteLine(SendTXT.Text);
                });
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            socket.Disconnect();
        }
    }
}
