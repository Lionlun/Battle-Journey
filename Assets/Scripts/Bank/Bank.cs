
using System;

public static class Bank
{
	public static event Action OnBankInitializedEvent;
	public static int Coins
	{ 
		get 
		{
			CheckClass();
			return bankInteractor.coins;
		}
	} 
	public static bool IsInitialized { get; private set; }

    private static BankInteractor bankInteractor;
    public static void Initialize(BankInteractor interactor)
    {
        bankInteractor = interactor;
		IsInitialized = true;
		OnBankInitializedEvent?.Invoke();

	}

	public static bool IsEnoughCoins(int value)
	{
		CheckClass();
		return bankInteractor.IsEnoughCoins(value);
	}

	public static void AddCoins(object sender, int value)
	{
		CheckClass();
		bankInteractor.AddCoins(sender, value);
	}

	public static void Spend(object sender, int value)
	{
		CheckClass();
		bankInteractor.Spend(sender, value);
	}
	private static void CheckClass()
	{
		if (!IsInitialized)
		{
			throw new System.Exception("Bank Not Initialized Yet");
		}
	}

}
