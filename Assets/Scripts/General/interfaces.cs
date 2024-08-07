namespace Intellectika.Interfaces
{

    public interface ILocatable  // интерфейс для объектов, которые должны помещаться в локатор
		{
        public UnityEngine.GameObject Return();

        public void OnDestroy();
        }

}
