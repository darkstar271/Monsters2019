using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpClassMovingMonsters.Business;
using CSharpClassMovingMonsters.Business.AllPunters;

namespace CSharpClassMovingMonsters
{
    public partial class Form1 : Form
    {    // Create my Monsters
        Monster[] monster = new Monster[4];
        Punter[] myPunters = new Punter[3];
        Punter CurrantPunter = new Howard();
        //  List<Monster> mym = new List<Monster>();
        //which monster wins
        private string MonsterWinner;


        public Form1()
        {
            InitializeComponent();
            LoadMonsters();
            LoadPunters();

        }

        #region Load the Monsters and Punters




        private void LoadMonsters()

        {
            //make an instance of our monster
            monster[0] = new Monster { Length = 0, myPB = pb1, Name = "Agor" };
            monster[0].myPB.BackgroundImage = Resource1.Agor;
            monster[1] = new Monster { Length = 0, myPB = pb2, Name = "Igor" };
            monster[1].myPB.BackgroundImage = Resource1.Igor;
            monster[2] = new Monster { Length = 0, myPB = pb3, Name = "Ogor" };
            monster[2].myPB.BackgroundImage = Resource1.Ogor;
            monster[3] = new Monster { Length = 0, myPB = pb4, Name = "Ugor" };
            monster[3].myPB.BackgroundImage = Resource1.Ugor;

        }



        private void LoadPunters()
        {
            for (int i = 0; i < 3; i++)
            {

                myPunters[i] = Factory.GetAPunter(i);
                //myPunter[0] which is howard is a new howard
                myPunters[i].LabelWinner = lblWinner;



            }

        }
        #endregion


        private void RunRace()
        {
            // while we haven't reached the end then keep looping
            bool end = false;
            // create a random number genarater
            Random myRand = new Random();
            Random myrandstop = new Random();
            while (end != true)
            {

                for (int i = 0; i < 4; i++)
                {
                    monster[i].myPB.Left += myRand.Next(1, 11);
                    //50% of the time it takes 8 off the left which makes it go back
                    if (myrandstop.Next(1, 3) == 2)
                    {
                        monster[i].myPB.Left -= 4;

                    }
                    // if the monster reaches the end of the form 
                    if (monster[i].myPB.Left > Form1.ActiveForm.Width - monster[i].myPB.Width - 40)
                    {
                        end = true; //loop until end  = true


                        this.Text = monster[i].Name + " the monster has won!!!";
                        //this is the monster that won the race
                        MonsterWinner = monster[i].Name;
                    }


                }
            }

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            RunRace();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {

            this.Text = "";
            // resets the left to 10 for the race
            for (int i = 0; i < 4; i++)
            {
                monster[i].myPB.Left = 10;

            }
        }

        private void AllRB_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton fakeRb = new RadioButton();

            fakeRb = (RadioButton)sender;




            if (fakeRb.Checked == true)
            {
                this.Text = fakeRb.Text;
                // look for the name of the person we have clicked on

                // pass the data across to currentPunter.
                switch (fakeRb.Text)
                {

                    case "Howard":

                        CurrantPunter = myPunters[0];
                        break;
                    case "Susan":
                        CurrantPunter = myPunters[1];
                        break;
                    case "John":
                        CurrantPunter = myPunters[2];
                        break;
                        //case "Howard":
                        //    CurrantPunter = new Howard();
                        //    break;




                }

                this.Text = CurrantPunter.PunterName + " is the currant Punter";

            }

            udBet.Maximum = (decimal)CurrantPunter.Cash;






        }

        private void BtnBets_Click(object sender, EventArgs e)
        {
            CurrantPunter.Bet = (float)udBet.Value;
            // this.Text = "Howard" + myPunters[0].Bet + " Susan " + myPunters[1].Bet + " John " + myPunters[2].Bet;
            lblBettorName.Text += CurrantPunter.PunterName + " Bets " + CurrantPunter.Bet + Environment.NewLine;
            //CurrantPunter.PunterName + " is the currant Punter and bet " + CurrantPunter.Bet;
        }
    }
}
