using GalaSoft.MvvmLight.Ioc;

namespace Calculator.ViewModel
{
    public static class Locator
    {
        static Locator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public static MainViewModel MainVM => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}