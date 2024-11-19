using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace G24W12WPFCardDealer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CardCount = 5;

        // 배열에 쓰일 상수
        private const int value = 0;
        private const int suit  = 1;
        public MainWindow() {
            InitializeComponent();
        }

        // 랜덤한 Value 값 뽑아내는 함수
        public static string GenerateValue() {
            string[] values = ["ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king"];

            Random rd = new();
            int index = rd.Next(0, values.Length);

            return values[index];
        }

        // 랜덤한 Suit 값 뽑아내는 함수
        public static string GenerateSuit() {
            string[] suits = ["spades", "diamonds", "hearts", "clubs"];

            Random rd = new();
            int index = rd.Next(0, suits.Length);

            return suits[index];
        }

        // 뭐뭐뭐_of_뭐뭐뭐를 "_of_"를 중심으로 내용만 뽑아내는 함수
        public static string[] GetSplit(string[] cardList, int index) {
            string[] temp    = cardList[index].Split(["_of_"], StringSplitOptions.None);
            string[] spltstr = [$"{temp[value]}", $"{temp[suit]}"];

            return spltstr;
        }

        private void OnDeal(object sender, RoutedEventArgs e)
        {
            //BitmapImage image = new BitmapImage(
            //    new Uri("Images/2_of_clubs.png", UriKind.Relative)
            //    );
            //Card1.Source = image;
            // 정렬 순서 정해주기
            var customOrder = new List<string> {
                    "ace_of_spades",    "ace_of_diamonds",    "ace_of_hearts",    "ace_of_clubs",
                    "2_of_spades",      "2_of_diamonds",      "2_of_hearts",      "2_of_clubs",
                    "3_of_spades",      "3_of_diamonds",      "3_of_hearts",      "3_of_clubs",
                    "4_of_spades",      "4_of_diamonds",      "4_of_hearts",      "4_of_clubs",
                    "5_of_spades",      "5_of_diamonds",      "5_of_hearts",      "5_of_clubs",
                    "6_of_spades",      "6_of_diamonds",      "6_of_hearts",      "6_of_clubs",
                    "7_of_spades",      "7_of_diamonds",      "7_of_hearts",      "7_of_clubs",
                    "8_of_spades",      "8_of_diamonds",      "8_of_hearts",      "8_of_clubs",
                    "9_of_spades",      "9_of_diamonds",      "9_of_hearts",      "9_of_clubs",
                    "10_of_spades",     "10_of_diamonds",     "10_of_hearts",     "10_of_clubs",
                    "jack_of_spades2",  "jack_of_diamonds2",  "jack_of_hearts2",  "jack_of_clubs2",
                    "queen_of_spades2", "queen_of_diamonds2", "queen_of_hearts2", "queen_of_clubs2",
                    "king_of_spades2",  "king_of_diamonds2",  "king_of_hearts2",  "king_of_clubs2" };

            var comparer = new CardComparer(customOrder);
            var cardSet  = new SortedSet<string>(comparer);

            // 중복 없이 카드 만들기
            while (cardSet.Count < CardCount)
            {
                string value = GenerateValue();
                string suit  = GenerateSuit();

                if (value is "jack" or "queen" or "king")
                    suit += "2";

                cardSet.Add($"{value}_of_{suit}");
            }

            string[] cardList = [.. cardSet];

            // 각 string에 맞는 이미지 지정
            int index = 0;

            BitmapImage image = new BitmapImage(new Uri($"Images/{cardList[index]}.png", UriKind.Relative));
            Card1.Source = image;
            index++;
            string[] card1arr = GetSplit(cardList, index);
            //TODO: 카드 4장 더 추가하기
        }

        // ChatGPT 참고해서 만든 Class
        public class CardComparer : IComparer<string> {
            private readonly Dictionary<string, int> _cardOrder;

            public CardComparer(IEnumerable<string> customOrder) {
                // 카드 값을 순서대로 매핑
                _cardOrder = [];
                int index  = 0;
                foreach (var card in customOrder) {
                    _cardOrder[card] = index++;
                }
            }

            public int Compare(string x, string y) {
                // 각 카드 값을 순서에 따라 비교
                return _cardOrder[x].CompareTo(_cardOrder[y]);
            }

        }
    }
}