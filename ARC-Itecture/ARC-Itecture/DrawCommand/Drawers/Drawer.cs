﻿using ARC_Itecture.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ARC_Itecture.DrawCommand.Drawers
{
    public abstract class Drawer
    {
        protected Brush _fillBrush;
        protected Brush _strokeBrush;
        protected Receiver _receiver;

        protected const int COMPONENT_OFFSET = 10;

        public Drawer(Receiver receiver)
        {
            this._receiver = receiver;
            this._fillBrush = new SolidColorBrush(ImageUtil.RandomColor());
            this._strokeBrush = new SolidColorBrush(Colors.White);
        }

        public abstract void Draw(Point p);

        public abstract void DrawPreview(Point p);

        protected Rectangle DrawRectangle(Point p1, Point p2)
        {
            Rectangle rectangle = new Rectangle
            {
                Fill = _fillBrush,
                Width = Math.Abs(p2.X - p1.X),
                Height = Math.Abs(p2.Y - p1.Y)
            };

            double leftMostX = p2.X > p1.X ? p1.X : p2.X;
            double topMostY = p2.Y > p1.Y ? p1.Y : p2.Y;
            Canvas.SetLeft(rectangle, leftMostX);
            Canvas.SetTop(rectangle, topMostY);

            _receiver.ViewModel._mainWindow.canvas.Children.Add(rectangle);

            return rectangle;
        }

        protected Line DrawSegment(Point p1, Point p2)
        {
            Line line = new Line
            {
                StrokeThickness = 1,
                Stroke = this._strokeBrush
            };

            double dX = Math.Abs(p2.X - p1.X);
            double dY = Math.Abs(p2.Y - p1.Y);

            line.X1 = p1.X;
            line.Y1 = p1.Y;

            if (dX > dY)
            {
                line.Y2 = p1.Y;
                line.X2 = p2.X;
            }
            else
            {
                line.X2 = p1.X;
                line.Y2 = p2.Y;
            }
            _receiver.ViewModel._mainWindow.canvas.Children.Add(line);

            return line;
        }
    }
}
