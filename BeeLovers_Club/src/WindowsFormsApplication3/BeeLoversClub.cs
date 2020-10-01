using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PictureBear.Visible = false; 
        }
        int timeingame = 0; //will be used to count seconds
        int time2 = 0; //will be used to count every 5 seconds
        int money; //current amount of money; grows every 5 seconds and when equal to 3$ - drops to 0$
        int flowers = 0; //amount of flowers; every time money==3, one flower is added
        int costofoneflower = 3; //one flower costs 3$
        int nectar = 0; //amount of nectar on flowers; grows in time (every second) depending on amount of flowers and drops every 10 seconds when collected by bees, depending on amount of bees
        int beeworkers; //amount of bees; grows in time
        int nectarstored = 0; //amount of stored nectar; grows every 10 seconds when bees collect it from flowers and drops when you store 900 grams which gives you one jar of honey
        int honeyproduced = 0; //amount of produced honey; honey is produced if you collect 900 grams of nectar
        int honeystored = 0; //amount of stored honey; equal to amount of produced honey minus honey taken by bears or Mark
        int honeyatonce; //amount of jars of honey produced at once - when playing for along time, speed can allow for that
        int value = 1; //will be used in a loop
        bool tradewithmark = false; //if you haven't made a trade with Mark yet - bears will come every time you store 4 jars of honey
        bool bears = false; //if bears haven't gave you a visit yet - when it happens for the first time - picture of bear will show up for a moment
        private void Timer1s_Tick(object sender, EventArgs e)
        {
            timeingame++;
            //amount of money in hand
            money = (timeingame / 5) - (flowers * costofoneflower);
            if (timeingame == 1)
            {
                MessageBox.Show("You've joined the BeeLoversClub. From now on, you will get a dollar every 5 seconds. But you can spend that money on flowers only.");
            }
            if (timeingame == 15)
            {
                MessageBox.Show("You've earned $3 which automatically trades for one flower. Let's wait for these little fellas now.");
            }
            if (timeingame==60)
            {
                MessageBox.Show("You have to be really patient with bees. It will take a while to see any results.");
            }
            if (timeingame==90)
            {
                MessageBox.Show("Really. Don't stare at the numbers. It takes 900 grams of nectar to produce one jar of honey.");
            }
            if (timeingame==150)
            {
                for (int i = 1; i <= 2; i++)
                {
                    MessageBox.Show("Bzzzzz");
                }
            }
            amountofmoney.Text = money + "$";
            /// if amount of money is bigger that the cost of one flower - it is exchanged for a flower
            if (money >= costofoneflower)
            {
                flowers = flowers + money / costofoneflower;
            }
            amountofflowers.Text = flowers.ToString();
            if (timeingame < 10) //next timer is set on 10 seconds so till that time these amounts has to be shown here
            {
                amountofhoney.Text = "0 jar(s)";
                amountofstoredn.Text = "0 g";
            }
            if (timeingame == 30)
            {
                MessageBox.Show("Oh, look! Smell of your flowers has lured a bee.");
            }
            /// below there is a simple animation; when below 30 seconds - there are no bees; later - bees are flying and taking nectar from flowers every 10 seconds
            if (timeingame < 30)
            {
            LabelAnimation.Text = "....";
            }
            if (timeingame >= 30)
            { 
                value++;
                if (value == 11)
                {
                    value = 1;
                }
                switch (value)
                    {
                        case 1:
                            LabelAnimation.Text = "...:";
                            break;
                        case 2:
                            LabelAnimation.Text = "....";
                            break;
                        case 3:
                            LabelAnimation.Text = "...:";
                            break;
                        case 4:
                            LabelAnimation.Text = "..:.";
                            break;
                        case 5:
                            LabelAnimation.Text = ".:..";
                            break;
                        case 6:
                            LabelAnimation.Text = ":...";
                            break;
                        case 7:
                            LabelAnimation.Text = "....";
                            break;
                        case 8:
                            LabelAnimation.Text = ":...";
                            break;
                        case 9:
                            LabelAnimation.Text = ".:..";
                            break;
                        case 10:
                            LabelAnimation.Text = "..:.";
                            break;
                    }
            }
            beeworkers = (timeingame / 30);
            amountofworkers.Text = beeworkers.ToString();
            ///speed of producing nectar depends on amount of flowers
            nectar = nectar + flowers * 2 / 3; 
            amountofnectar.Text = nectar + " g";
            ///below there are titles depending on your progress in the game
            if ((beeworkers >= 1) & (honeyproduced == 0))
            {
                rank.Text = "Maya The Bee";
            }
            else if ((honeyproduced >= 1) & (honeyproduced==honeystored))
            {
                rank.Text = "As busy as a bee";
            }
            else if ((honeyproduced > honeystored) & tradewithmark == false)
            {
                rank.Text = "'Bears love honey and I am a Pooh bear'";
            }
            else if ((tradewithmark == true) & (honeystored <= 4))
            {
                rank.Text = "Not really a businessman";
            }
            else if ((honeystored > 4) & (honeystored <= 10))
            {
                rank.Text = "The Queen Bee";
            }
            else if (honeystored > 10)
            {
                rank.Text = "Just a No-life";
            }
        }
        private void Timer10s_Tick(object sender, EventArgs e)
        {
        /// if bears have appeared, the image won't show up anymore
        if (bears == true)
        {
            PictureBear.Visible = false;
        }
        time2++;
        ///amount of stored nectar depends on amount of bees
        nectarstored = nectarstored + (beeworkers * 9);
        ///amoount of nectar on flowers drops when bees collect it
        nectar = nectar - beeworkers * 9;
        ///when amount of stored nectar reaches 900 grams, it is exchanged for one jar of honey
        if (nectarstored >= 900)
        {
            honeyatonce = nectarstored / 900;
            honeyproduced = honeyproduced + honeyatonce;
            honeystored = honeystored + honeyatonce;
            nectarstored = nectarstored - (900 * honeyatonce);
        }
        amountofstoredn.Text = nectarstored + " g";
        amountofhoney.Text = honeystored + " jar(s)";
            if (time2 == 26)
        {
            MessageBox.Show("Finally, you have your first jar of honey. Mark, your neighbour, wants to make a trade with you. He told you to call him when you produce 3 jars of honey. He promised to give you 'A really special hive' he bought recently. But if you ask me, he seems kind of shifty.");
        }
        ///if you haven't called Mark, bears will eat all your supply of honey every time you have at least jars of honey; first visit will also result in showing a hidden image
        if ((honeystored >= 4) & (tradewithmark == false))
        {
            honeystored = 0;
            MessageBox.Show("Unfortunatelly, your honey is also admired by bears. One of them came and ate your entire honey supply.");
            PictureBear.Visible = true;
            bears = true;
        }
        }
        /// <summary>
        /// when you call Mark and haven't spoken with him before or don't have enough jars - nothing happens, only message box apperar
        ///when you call Mark to make a trade with him, he takes your 3 jars of honey but bears won't appear anymore
        ///after the trade you can't contact him anymore - message box will show up
        /// </summary>
        private void ButtonPhone_Click(object sender, EventArgs e)
        {
            if (honeyproduced < 1)
            {
                MessageBox.Show("Mark? Who is Mark?");
            }
            else if ((honeystored < 3) & (tradewithmark == false))
            {
                MessageBox.Show("You don't have enough jars. Mark asked you for 3.");
            }
            else if ((honeystored == 3) & (tradewithmark == false))
            {
                honeystored = honeystored - 3;
                tradewithmark = true;
                MessageBox.Show("Mark took the jars and promised to deliver the hive later... When he was leaving in rush you saw a few bears following him.");
            }
            else if (tradewithmark == true)
            {
                MessageBox.Show("Nobody's answering. I think you won't get your jars back.");
            }
        }
    }  
}

