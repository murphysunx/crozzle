using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    class WebBrowserDisplay
    {
        private string content;

        public WebBrowserDisplay()
        {
            content = @"<!DOCTYPE html>
                                <html>
                                <head>
                                <style>
                                table {
                                    width:360px;
                                }
                                table, td {
                                    border: 1px solid black;
                                    border-collapse: collapse;
                                }
                                td {
                                    width:24px;
                                    text-align: center;
                                }
                                </style>
                                </head>
                                <body>
                                <table>";
        }

        public void AddContent(string str)
        {
            content += str;
            return;
        }

        public string GetContent()
        {
            return content;
        }

        public void ClearContent()
        {
            content = @"<!DOCTYPE html>
                                <html>
                                <head>
                                <style>
                                table {
                                    width:360px;
                                }
                                table, td {
                                    border: 1px solid black;
                                    border-collapse: collapse;
                                }
                                td {
                                    width:24px;
                                    text-align: center;
                                }
                                </style>
                                </head>
                                <body>
                                <table>";
            return;
        }
    }
}
