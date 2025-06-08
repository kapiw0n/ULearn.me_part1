public static GameResult GetGameResult(Mark[,] field)
{
	bool CrossWin = false;
	bool CircleWin = false;

	for (int i = 0; i< 3; i++)
 	{
 		if (field[i,0] == field[i,1] && field[i,1] == field[i,2])
 		{
 			if (field[i,0] == Mark.Cross) CrossWin = true;
 			else if (field[i,0] == Mark.Circle) CircleWin = true;
 		}

		if (field[0,i] == field[1,i] && field[1,i] == field[2,i])
 		{
 			if (field[0,i] == Mark.Cross) CrossWin = true;
 			else if (field[0,i] == Mark.Circle) CircleWin = true;
 		}
 	}

	if ((field[0,0] == field[1,1] && field[1,1] == field[2,2]) || (field[2,0] == field[1,1] && field[1,1] == field[0,2]))
 	{
 		if (field[1,1] == Mark.Cross) CrossWin = true;
 		else if (field[1,1] == Mark.Circle) CircleWin = true;
 	}

	if ((CrossWin && CircleWin) || (!CrossWin && !CircleWin))
    {
        return GameResult.Draw;
    }
    
	else if (CrossWin)
    {
        return GameResult.CrossWin;
    }

	else
    {
        return GameResult.CircleWin;
    }
}