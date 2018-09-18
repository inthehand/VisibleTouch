using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace InTheHand
{
    public sealed class TouchWindow : UIWindow
    {     
        public TouchWindow(CGRect bounds) : base(bounds)
        {
        }

        public override void SendEvent(UIEvent evt)
        {
            NSSet touches = evt.AllTouches;

            foreach (UITouch touch in touches)
            {
                switch (touch.Phase)
                {
                    case UITouchPhase.Began:
                        TouchBegan(touch);
                        break;

                    case UITouchPhase.Moved:
                        TouchMoved(touch);
                        break;

                    case UITouchPhase.Ended:
                    case UITouchPhase.Cancelled:
                        TouchEnding(touch);
                        break;
                }
            }

            base.SendEvent(evt);
        }

        CAShapeLayer circleLayer;

        void ShowCircleAtPosition(CGPoint position)
        {
            circleLayer = new CAShapeLayer();
            circleLayer.Position = new CGPoint(position.X - 20, position.Y - 20);
            UIBezierPath startPath = UIBezierPath.FromOval(new CGRect(0, 0, 40, 40));
            circleLayer.Path = startPath.CGPath;
            circleLayer.FillColor = UIColor.FromRGBA(0xff, 0xff, 0xff, 0x80).CGColor;
            circleLayer.StrokeColor = UIColor.FromRGBA(0x00, 0x00, 0x00, 0x80).CGColor;
            circleLayer.LineWidth = 2.0f;
            Layer.AddSublayer(circleLayer);
        }

        public void TouchBegan(UITouch touch)
        {
            ShowCircleAtPosition(touch.LocationInView(this));
        }

        public void TouchMoved(UITouch touch)
        {
            if (circleLayer != null)
            {
                CGPoint position = touch.LocationInView(this);
                circleLayer.Position = new CGPoint(position.X - 30, position.Y - 30);
            }
        }

        public void TouchEnding(UITouch touch)
        {
            circleLayer?.RemoveFromSuperLayer();
            circleLayer = null;
        }
    }
}