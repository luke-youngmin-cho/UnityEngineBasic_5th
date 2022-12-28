public interface IBuff<T>
{
    void OnActive(T subject);
    void OnDuration(T subject);
    void OnDeactive(T subject);
}