namespace ETHotfix
{
    public enum TouchEvent
    {
        Down,
        Up,
        Press
    }

    //��ע��İ���
    public enum TOUCH_KEY
    {
        Idle = 0,
        Run = 1 << 0,
        Attack = 1 << 1,
        Skill1 = 1 << 2,
        Skill2 = 1 << 3,
        Skill3 = 1 << 4,
        Skill4 = 1 << 5,
        Summon1 = 1 << 6, //�ٻ�ʦ����1
        Summon2 = 1 << 7, //�ٻ�ʦ����2
        Summon3 = 1 << 8, //�س�
    }

    public interface ITouchHandler
    {
        void Clear();

        bool IsTouched(TOUCH_KEY key);

        void Touch(TOUCH_KEY key);

        void Release(TOUCH_KEY key);
    }

    //���������ӿ�
    public class TouchHandler : ITouchHandler
    {
        private static TouchHandler instance = null;
        private int mKeyValue; //���浱ǰ��λ
        public delegate void KeyChanged(TouchEvent e, TOUCH_KEY key); //ע��ί������
        public KeyChanged OnKeyChanged; //ע��ί�з���

        public static TouchHandler GetInstance()
        {
            if (instance == null)
            {
                instance = new TouchHandler();
                instance.mKeyValue = 0;
            }
            return instance;
        }

        private TouchHandler()
        {
        }

        public void Clear() //�����λ
        {
            mKeyValue = 0;
        }

        public bool IsTouched(TOUCH_KEY key) //�Ƿ��Ѱ���
        {
            return IsTouched(key, mKeyValue);
        }

        public bool IsTouched(TOUCH_KEY key, int compKey)
        {
            return (compKey & (int)key) != 0;
        }

        public void Touch(TOUCH_KEY key) //����
        {
            Touch(key, ref mKeyValue);
        }

        public void Touch(TOUCH_KEY key, ref int compKey)
        {
            if (OnKeyChanged != null && !IsTouched(key))
                OnKeyChanged(TouchEvent.Down, key);
            compKey |= (int)key;
        }

        public void Release(TOUCH_KEY key) //�ɿ�
        {
            Release(key, ref mKeyValue);
        }

        public void Release(TOUCH_KEY key, ref int compKey)
        {
            if (OnKeyChanged != null && IsTouched(key))
                OnKeyChanged(TouchEvent.Up, key);
            compKey &= ~(int)key; //0&0Ϊ0  1&0Ϊ0
        }
    }
}
