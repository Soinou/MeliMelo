using MeliMelo.Core.Shortcuts;
using MeliMelo.Core.Tasks;
using System;
using System.Windows.Forms;

namespace MeliMelo.iTunes
{
    public class iTunesTask : IAutoTask
    {
        public iTunesTask(IKeyboardManager keyboard_manager)
        {
            keyboard_manager_ = keyboard_manager;
            running_ = false;
            page_up_id_ = 0;
            page_down_id_ = 0;
            insert_id_ = 0;

            keyboard_manager_.KeyPressed += OnKeyboardManagerKeyPressed;
        }

        public string Name
        {
            get
            {
                return "iTunes";
            }
        }

        public bool Running
        {
            get
            {
                return running_;
            }
        }

        public void Run()
        {
            // Nothing to do here
        }

        public void Start()
        {
            if (!running_)
            {
                page_up_id_ = keyboard_manager_.Register(Modifiers.kAlt | Modifiers.kControl,
                    Keys.PageUp);
                page_down_id_ = keyboard_manager_.Register(Modifiers.kAlt | Modifiers.kControl,
                    Keys.PageDown);
                insert_id_ = keyboard_manager_.Register(Modifiers.kAlt | Modifiers.kControl,
                    Keys.Insert);

                running_ = true;
            }
        }

        public void Stop()
        {
            if (running_)
            {
                keyboard_manager_.Unregister(page_up_id_);
                keyboard_manager_.Unregister(page_down_id_);
                keyboard_manager_.Unregister(insert_id_);

                running_ = false;
            }
        }

        public event EventHandler OnNext;

        public event EventHandler OnPlayPause;

        public event EventHandler OnPrevious;

        protected void OnKeyboardManagerKeyPressed(object sender, KeyPressedEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.PageUp:
                    if (OnNext != null)
                        OnNext(this, new EventArgs());
                    break;

                case Keys.PageDown:
                    if (OnPrevious != null)
                        OnPrevious(this, new EventArgs());
                    break;

                case Keys.Insert:
                    if (OnPlayPause != null)
                        OnPlayPause(this, new EventArgs());
                    break;

                default:
                    break;
            }
        }

        protected int insert_id_;
        protected IKeyboardManager keyboard_manager_;
        protected int page_down_id_;
        protected int page_up_id_;
        protected bool running_;
    }
}
