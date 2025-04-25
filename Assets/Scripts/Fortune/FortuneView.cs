namespace Fortune
{
    public class FortuneView : View<FortuneMainModel, FortuneMainController>
    {
        public void FinishAnimation()
        {
            m_controller.CloseAnimator();
        }
    }
}