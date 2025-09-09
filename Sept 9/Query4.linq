<Query Kind="Program" />

void Main()
{
	int number1 = 10;
	int number2 = 10;
	UseIfStatement(number1, number2).Dump();
	UseTernaryOperator(number1, number2).Dump();
}

//  if statement
public int UseIfStatement(int x, int y)
{
	if(x > y)
	{
		return x;
	}
	else
	if(y > x)
	{
		return y;
	}
	else
	{
		return 0;
	}
}

public string UseTernaryOperator(int x, int y)
{
	return x > y ? "x" 
				 : y > x ? "y"
				 : "same";
}


