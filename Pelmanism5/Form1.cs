using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pelmanism5
{
    public partial class Form1 : Form
    {
        private Card[] playingCard;
        private Player player;
        private int gamesec;

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateCard(ref Card[] cards)
        {
            string[] picture =
            {
                "〇", "●", "△", "▲", "□", "■", "◇","◆", "☆",
 "★","※","×"
            };

            cards = new Card[picture.Length * 2];
            for (int i = 0, j = 0; i < cards.Length; i += 2, j++)
            {
                cards[i] = new Card(picture[j]);
                cards[i + 1] = new Card(picture[j]);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateCard(ref playingCard);
            player = new Player();

            //カードを動的に配置
            SuspendLayout();//レイアウト処理停止
            const int offsetX = 30, offsetY = 50;
            for(int i=0; i<playingCard.Length; i++)
            {
                playingCard[i].Name = "card" + i;
                int sizeW = playingCard[i].Size.Width;
                int sizeH = playingCard[i].Size.Height;
                playingCard[i].Location = new Point(
                    offsetX + i % 8 * sizeW, offsetY + i / 8 * sizeH);//式の意味
                playingCard[i].Click += new EventHandler(
                    ButtonCard_Click);
            }
            Controls.AddRange(playingCard);
            //true:今すぐレイアウト実行
            //false:レイアウト停止を解除、フォーム表示時にまとめてレイアウト
            //SuspendLayout()呼び出したら必ず書く
            ResumeLayout(false);

            label2.Text = "スタートで開始";
        }

        private bool MatchCard(Card[] cards, int index1,int index2)
        {
            if (index1 < 0 || index1 >= cards.Length ||
                index2 < 0 || index2 >= cards.Length)
                return false;

            if (cards[index1].Picture == cards[index2].Picture)
                return true;
            else
                return false;
        }

        private bool AllCardOpen(Card[] cards)//[]ないとどうなるか
        {
            foreach(Card c in playingCard)
            {
                if (c.State == false)
                    return false;
            }
            return true;
        }

        private void ButtonCard_Click(object sender, EventArgs e)
        {
            if (player.OpenCounter == 0)
            {
                int b1 = player.BeforeOpenCardIndex1;
                int b2=player.BeforeOpenCardIndex2;
                if(b1!=-1 && b2!=-1 && MatchCard(playingCard,b1, b2) == false)
                {
                    playingCard[b1].Close();
                    playingCard[b2].Close();
                }

                int n1 = int.Parse(((Button)sender).Name.Substring(4));
                playingCard[n1].Open();
                player.NowOpenCardIndex1 = n1;
            }
            else if(player.OpenCounter == 1)
            {
                int n2 = int.Parse(((Button)sender).Name.Substring(4));
                playingCard[n2].Open();
                player.NowOpenCardIndex2 = n2;

                if (MatchCard(playingCard, player.NowOpenCardIndex1,
                    player.NowOpenCardIndex2))
                    label2.Text = "一致！";
                else
                    label2.Text = "不一致...";

                player.Reset();

                if (AllCardOpen(playingCard))
                {
                    timer1.Stop();
                    label2.Text = "おめでとう！";
                    button1.Enabled = true;
                }
            }
        }

        private void ShuffleCard(Card[] cards)
        {
            Random r=new Random();
            int n = cards.Length - 1;

            while (n > 0)
            {
                int w = r.Next(0, n);
                string s = playingCard[n].Picture;
                playingCard[n].Picture = playingCard[w].Picture;
                playingCard[w].Picture = s;
                n--;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShuffleCard(playingCard);
            timer1.Start();


            foreach(Card c in playingCard)
            {
                c.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gamesec++;
            label1.Text =gamesec+ "秒経過";
        }
    }
}
