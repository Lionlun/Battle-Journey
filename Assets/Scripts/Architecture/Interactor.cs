
public abstract class Interactor
{
	public virtual void OnCreate() { } //ѕосле создани€ все репозиториев и интеракторов

	public virtual void Initialize() { } // ѕосле OnCreate() всех репозиториев и интерокторов
	public virtual void OnStart() { } //ѕосле инициализации всех репо и интеракторов
}
