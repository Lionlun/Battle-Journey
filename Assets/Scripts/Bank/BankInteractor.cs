
public class BankInteractor : Interactor
{
	private BankRepository repository;
	public int coins => this.repository.coins;

	public override void OnCreate()
	{
		base.OnCreate();
		this.repository = ArchitectureTest.repositoriesBase.GetRepository<BankRepository>();
	}

	public override void Initialize()
	{
		Bank.Initialize(this);
	}

	public bool IsEnoughCoins(int value)
	{
		return coins >= value;
	}

	public void AddCoins(object sender, int value)
	{
		this.repository.coins += value;
		this.repository.Save();
	}

	public void Spend(object sender, int value)
	{
		this.repository.coins -= value;
		this.repository.Save();
	}
}
