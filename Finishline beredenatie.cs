public bool check1, check2, check3, check4;		//Dit zijn gewoon ff placeholders voor de checkpoints.
public bool pitStop;							//Dit is gewoon ff placeholder voor de Pitstop.
Form2 frm2 = new Form2();						//Het eindscherm.
frm2.Visible = false;
Stopwatch stopWatch = new Stopwatch();			//Een stopwatch voor Player 1.
Stopwatch stopHorloge = new Stopwatch();		//Een stopwatch voor Player 2.
public int LapP1 = 0;							//Laps voor Player 1.
public int LapP2								//Laps voor Player 2.
public bool a = false;							//Zorgt ervoor dat als beide (a en b) true zijn dat het spel afsluit.
public bool b = false;



//Er moeten nog dingen gebeuren vanuit *checkpoint* en *pitStop* en natuurlijk moet er interactie zijn met de players 1 en 2.
//Het hele systeem van de stopwatchen staat in het bestand: *Player Logic*.

Player 1:
			Finish:		//Als player 1 over de finish lijn gaat dan checkt dit systeem of player 1 niet cheat.
						if(check1 && check2 && check3 && check4)
						{
							LapP1++;
							if(LapP1 == 1)
							{
								label1.Text = String.Format("{0}", stopWatch.Elapsed);
								stopWatch.Restart();
								
								check1 = false;
								check2 = false;
								check3 = false;
								check4 = false;
							}
							
							if(LapP1 == 2)
							{
								label2.Text = String.Format("{0}", stopHorloge.Elapsed);
								stopWatch.Restart();
								
								check1 = false;
								check2 = false;
								check3 = false;
								check4 = false;
							}
							
							if(LapP1 == 3)
							{
								a = true;
								label3.Text = String.Format("{0}", stopHorloge.Elapsed);
								stopWatch.Stop();
							}
							if(a)
							{break;}
						}
						
						if(a && b)
						{
							frm2.Visible = true;
						}
						
						
Player 2:
			Finish:		//Zelfde verhaal als voor player 1 maar nu voor player 2.
						if(check1 && check2 && check3 && check4)
						{
							LapP2++;
							
							if(LapP2 == 1)
							{
								label4.Text = String.Format("{0}", stopHorloge.Elapsed);
								stopHorloge.Restart();
							}
							
							if(LapP2 == 2)
							{
								label5.Text = String.Format("{0}", stopHorloge.Elapsed);
								stopHorloge.Restart();
							}
							
							if(LapP2 == 3)
							{
								b = true;
								label6.Text = String.Format("{0}", stopHorloge.Elapsed);
								stopHorloge.Stop();
							}
							if(b)
							{break;}
						}
						
						if(a && b)
						{
							frm2.Visible = true;
						}
						
						
						
						
