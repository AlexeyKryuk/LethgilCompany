using Core.Model;

namespace Core.View
{
    public interface IAttachableView
    {
        void Attach(IGrabberView grabber);
        void Render(bool isReady);
        void Drop(float power);

        LootType Type { get; }
        int PhotonViewId { get; }
        bool IsAvailable { get; }
    }
}
