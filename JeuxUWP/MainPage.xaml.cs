using Jeux;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace JeuxUWP
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool isExit = false;

        List<string> startList = new List<string>();
        List<string> musicList = new List<string>();
        List<string> foodList = new List<string>();
        List<string> lastWordList = new List<string>();
        List<string> randomList = new List<string>();

        //깜박임 현상을 줄이기 위해 미리 할당
        BitmapImage jeux = new BitmapImage(new Uri("ms-appx:///resource/10손제욱.jpg"));
        BitmapImage jeux2 = new BitmapImage(new Uri("ms-appx:///resource/10손제욱2.png"));
        BitmapImage foot = new BitmapImage(new Uri("ms-appx:///resource/발.jpg"));
        BitmapImage serious = new BitmapImage(new Uri("ms-appx:///resource/심각.png"));
        BitmapImage yee = new BitmapImage(new Uri("ms-appx:///resource/Yee.png"));
        BitmapImage sing = new BitmapImage(new Uri("ms-appx:///resource/10손제욱_sing.png"));

        bool singing = false;
        bool isNormalFace = true;
        bool isExitDay = false;
        bool isLastWordGame = false;
        string timeStr;
        int classCount;
        int jeuk = 0;

        DispatcherTimer faceTimer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1600, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            StartSetting();
            SetRandomList();
            try
            {
                SetStartLabel();
            }
            catch (Exception)
            {
            
            }
            
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Start();
            timer.Tick += (s, e) =>
            {
                UpdateTime();
            };
            faceTimer.Interval = TimeSpan.FromMilliseconds(300);
            faceTimer.Tick += (s, e) => { FaceTimer_Tick(); };
            AddLastWordList();
        }
        private void SetRandomList()
        {
            randomList.Add("엄마 없어?");
            randomList.Add("하..하...");
            randomList.Add("헤ㅔㅔ~?");
            randomList.Add("왜 살아?");
            randomList.Add("아니 저기요");
            randomList.Add("선.생.님. 뭐하시는 거죠?");
            randomList.Add("죽고싶어?");
            randomList.Add("디질래?");
            randomList.Add("진짜 엄마 없어?");
        }
        private void FaceTimer_Tick()
        {
            if (!isExitDay && !singing)
            {
                if (isNormalFace) mainImage.Source = jeux2;
                else mainImage.Source = jeux;
                isNormalFace = !isNormalFace;
            }

        }

        private void UpdateTime()
        {
            int hour = int.Parse(DateTime.Now.ToString("hh"));
            int minute = int.Parse(DateTime.Now.ToString("mm"));
            int second = int.Parse(DateTime.Now.ToString("ss"));
            string ap = DateTime.Now.ToString("tt");

            //int check = 0;

            timeStr = DateTime.Now.ToLongTimeString();
            timeLabel.Text = timeStr;
            for (int i = 8; i < 16; i++)
            {
                int temp1 = i % 12;
                int temp2 = i % 12 + 1;
                Class classTime = new Class(temp1, temp2, 0);
                classCount = classTime.compare(hour);

                if (classCount > 0)
                {
                    //mainLabel.Text = remainMinute(hour,minute);
                    jeuk = RemainMinute(hour, minute);
                    //if (jeuk != -1) mainLabel.Text = jeuk + "분 남았습니다.";
                    if (jeuk == -1) timeLabel.Text = timeStr + "\n수업 일과 시간이 아닙니다.";
                    break;
                }
            }
            if (classCount != -1 && jeuk != -1)
                timeLabel.Text = timeStr + "\n" + classCount + "교시 " + jeuk + "분 남았습니다.";
        }

        public int RemainMinute(int nowHour, int nowMinute)
        {

            if (nowHour >= 8 && nowHour <= 9 && (nowMinute >= 40 || nowMinute <= 30))
            {
                if (nowHour < 9)
                {
                    return 60 - nowMinute + 30;

                }
                else
                {
                    return 30 - nowMinute;

                }
            }
            else if (nowHour >= 9 && nowHour <= 10 && (nowMinute <= 50 || nowMinute >= 30))
            {
                if (nowHour < 10)
                {

                    return 60 - nowMinute + 30;

                }
                else
                {

                    return 30 - nowMinute;

                }
            }
            else if (nowHour >= 10 && nowHour <= 11 && (nowMinute >= 50 || nowMinute <= 40))
            {
                if (nowHour < 11)
                {

                    return 60 - nowMinute + 40;

                }
                else
                {

                    return 40 - nowMinute;

                }
            }
            else if (nowHour >= 11 && nowHour <= 12 && (nowMinute >= 50 || nowMinute <= 40))
            {
                if (nowHour < 12)
                {

                    return 60 - nowMinute + 40;
                }
                else
                {

                    return 40 - nowMinute;
                }
            }
            else if (nowHour >= 1 && nowHour <= 2 && (nowMinute >= 30 && nowMinute <= 20))
            {
                if (nowHour < 2)
                {
                    return 60 - nowMinute + 20;
                }
                else
                {
                    return 20 - nowMinute;
                }
            }
            else if (nowHour >= 2 && nowHour <= 3 && (nowMinute >= 30 || nowMinute <= 20))
            {
                if (nowHour < 3)
                {
                    return 60 - nowMinute + 20;
                }
                else
                {
                    return 20 - nowMinute;
                }
            }
            else if (nowHour >= 3 && nowHour <= 4 && (nowMinute >= 30 || nowMinute <= 20))
            {
                if (nowHour < 4)
                {
                    return 60 - nowMinute + 20;
                }
                else
                {
                    return 20 - nowMinute;
                }
            }
            else return -1;
        }
        public void StartSetting()
        {
            startList.Add("안녕하십니까 사용자님");
            musicList.Add("안녕하십니까 사용자님.mp3");
            startList.Add("봉주르 사용자님");
            musicList.Add("봉주르 사용자님.mp3");
            startList.Add("심심하신가요?");
            musicList.Add("심심하신가요.mp3");
            startList.Add("저랑 대화하고 싶으신 가요?");
            musicList.Add("저랑 대화하고 싶으신가요.mp3");
        }

        public void SetStartLabel()
        {
            Random random = new Random();
            int index = random.Next(startList.Count);
            PlaySoundAsync(musicList[index]);
            mainLabel.Text = startList[index];
        }

        public void SetManual()
        {
            cmdList.Text = "급식 : 급식을 띄워줍니다.\n"
                + "꺼져 : 프로그램을 종료합니다.\n"
                + "비켜 : 윈도우를 최소화 합니다.\n"
                + "노래해줘 : 제욱스가 노래를 불러줍니다.\n"
                + "끝말잇기 : 제욱스와 하는 마성의 \n\t   끝말잇기\n"
                + "퇴사 : (감옥) 출소일(?)을 알려줍니다.";
        }

        public async void PlaySoundAsync(string name)
        {
            try
            {
                StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("voice");
                StorageFile file = await folder.GetFileAsync(name);
                var stream = await file.OpenAsync(FileAccessMode.Read);
                mediaPlayer.SetSource(stream, file.ContentType);
                mediaPlayer.Play();
                faceTimer.Start();

            }
            catch (Exception e)
            {
                string msg = e.Message;
                Debug.WriteLine(msg);
            }
            
        }

        public async void PlaySoundOffAsync(string name)
        {
            try
            {
                mediaPlayer.MediaEnded += Exit;
                StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("voice");
                StorageFile file = await folder.GetFileAsync(name);
                var stream = await file.OpenAsync(FileAccessMode.Read);
                mediaPlayer.SetSource(stream, file.ContentType);
                mediaPlayer.Play();
                faceTimer.Start();
                
            }
            catch (Exception e)
            {
                string msg = e.Message;
                Debug.WriteLine(msg);
            }
            
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private async void EnterKeyPressedAsync(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                string answer = answerBox.Text.ToLower();
                if (isLastWordGame)
                {
                    if (answer.Equals("졌다."))
                    {
                        isLastWordGame = false;
                    }
                    if (answer.Equals("꺼져"))
                    {
                        PlaySoundAsync("프로그램을 종료합니다(시무룩_).mp3");
                        isExit = true;
                        answerBox.Text = "";
                    }
                    if (answer.Equals("")) return;
                    string str = answer.Substring(answer.Length - 1);

                    for (int i = 0; i < lastWordList.Count; i++)
                    {
                        if (lastWordList[i].IndexOf(char.Parse(str)) == 0)
                        {
                            mainLabel.Text = lastWordList[i];
                            ContentDialog content = new ContentDialog()
                            {
                                Content = "패배를 인정하려면 졌다를 쳐주세요^つ^",
                                CloseButtonText = "확인"
                            };
                            await content.ShowAsync();
                        }
                    }
                }
                else if (answer.Contains("급식"))
                {
                    Jeux.Program parser = new Jeux.Program();
                    await parser.settingAsync();
                    resultText.Text = parser.getFood();
                    PlaySoundAsync("급식충에게 급식이란.mp3");
                    mainLabel.Text = "급식충에게 급식이란....";
                    answerBox.Text = "";
                }

                else if (answer.Contains("심심") || answer.Contains("놀아줘") || answer.Contains("끝말잇기"))      // 끝말잇기
                {
                    Random rand = new Random();
                    int randResult = rand.Next(2);
                    if (randResult == 0)
                    {
                        PlaySoundAsync("놀아드릴까요.mp3");
                        mainLabel.Text = "놀아드릴까요?";
                    }
                    else if (randResult == 1)
                    {
                        PlaySoundAsync("심심하신가요.mp3");
                        mainLabel.Text = "심심하신가요?";
                    }
                    ContentDialog content = new ContentDialog()
                    {
                        Title = "야생의 제욱스가 싸움을 걸어왔다!",
                        Content = "제욱스와 끝말잇기를 하시겠습니까?",
                        PrimaryButtonText = "예",
                        CloseButtonText = "아니요"
                    };
                    ContentDialogResult result = await content.ShowAsync();
                    if (result.Equals(ContentDialogResult.Primary))
                    {
                        mainLabel.Text = "먼저 하시죠.";
                        isLastWordGame = true;
                    }
                    answerBox.Text = "";
                }

                else if (answer.Contains("제욱") || answer.Contains("ㅎㅇ") || answer.Contains("하이") || answer.Contains("하이루") || answer.Contains("헬로") || answer.Contains("안녕") || answer.Contains("봉주르") || answer.Contains("Hi") || answer.Contains("Hello"))
                {
                    Random rand = new Random();
                    int randResult = rand.Next(3);
                    if (randResult == 0)
                    {
                        PlaySoundAsync("봉주르 사용자님.mp3");
                        mainLabel.Text = "봉주르! 사용자님";
                    }
                    else if (randResult == 1)
                    {
                        PlaySoundAsync("봉주르.mp3");
                        mainLabel.Text = "봉주르~~";
                    }
                    else
                    {
                        PlaySoundAsync("달팽이 요리 드셔보실래요.mp3");
                        mainLabel.Text = "달팽이 요리 드셔보실래요?";

                    }
                    answerBox.Text = "";
                }

                else if (answer.Contains("극혐") || answer.Contains("fuck") || answer.Contains("sibal") || answer.Contains("시발") || answer.Contains("애미") || answer.Contains("씨발") || answer.Contains("ㅗ") || answer.Contains("ㅗ") || answer.Contains("ㅛ") || answer.Contains("凸") || answer.Contains("😝") || answer.Contains("😜") || answer.Contains("😛") || answer.Contains("🖕"))
                {
                    Random rand = new Random();
                    int randResult = rand.Next(4);
                    if (randResult == 0)
                    {
                        PlaySoundAsync("시_무_룩.mp3");
                        mainLabel.Text = "시__무__룩";
                    }
                    else if (randResult == 1)
                    {
                        PlaySoundAsync("아니저기요 사용자님 뭐하시는거죠 진짜 죽고싶어.mp3");
                        mainLabel.Text = "아니저기요 사용자님 -.- \n뭐하시는거죠 진짜 죽고싶어?";
                    }
                    else if (randResult == 2)
                    {
                        PlaySoundAsync("진짜 왜살아 엄마없어.mp3");
                        mainLabel.Text = "진짜 왜살아 엄마없어?";

                    }
                    else
                    {
                        PlaySoundAsync("왜그러시죠.mp3");
                        mainLabel.Text = "왜그러시죠?";
                    }
                    answerBox.Text = "";
                }

                else if (answer.Contains("노래해줘"))
                {
                    mainImage.Source = sing;
                    PlaySoundAsync("에반스.mp3");
                    singing = true;
                    answerBox.Text = "";
                }

                else if (answer.Contains("꺼져"))
                {
                    PlaySoundAsync("프로그램을 종료합니다(시무룩_).mp3");
                    isExit = true;
                    answerBox.Text = "";
                }

                else if (answer.Contains("비켜"))
                {
                    //TODO 최소화

                    PlaySoundAsync("에~~~~~.mp3");
                    answerBox.Text = "";
                }

                else if (answer.Equals("man"))
                {
                    SetManual();
                    answerBox.Text = "";
                }

                else if (answer.Equals("yee"))
                {
                    mainImage.Source = yee;
                    PlaySoundAsync("Yee.mp3");
                    singing = true;
                    answerBox.Text = "";
                }

                else if (answer.Equals("디데이") || answer.Contains("퇴사"))
                {
                    bool ExitDay = false;
                    DateTime dtToday = DateTime.Now;

                    CultureInfo ciCurrent = Thread.CurrentThread.CurrentCulture;
                    DayOfWeek dwFirst = ciCurrent.DateTimeFormat.FirstDayOfWeek;
                    DayOfWeek dwToday = ciCurrent.Calendar.GetDayOfWeek(dtToday);

                    DateTime dtLastDayOfThisWeek = dtToday.AddDays(-(dwToday - dwFirst) + 5);
                    while (!ExitDay)
                    {
                        Jeux.Program parser = new Jeux.Program();
                        await parser.settingAsync(dtLastDayOfThisWeek.Month, dtLastDayOfThisWeek.Day);
                        ExitDay = parser.getExitDay();
                        if (ExitDay)
                            break;
                        dtLastDayOfThisWeek = dtLastDayOfThisWeek.AddDays(7);
                        Debug.WriteLine(dtLastDayOfThisWeek.Day);
                    }

                    mainImage.Source = serious;

                    PlaySoundAsync("하루라도 빨리 나가고 싶으신가 보네요.mp3");

                    isExitDay = true;

                    mainLabel.Text = "하루라도 빨리 나가고\n싶으신가 보네요";
                    resultText.Text = "오늘\n" + DateTime.Now.Year + "년 " + DateTime.Now.Month + "월 " + DateTime.Now.Day + "일\n\n퇴사일\n" + dtLastDayOfThisWeek.Year + "년 " + dtLastDayOfThisWeek.Month + "월 " + dtLastDayOfThisWeek.Day + "일\n\n남은 기간 " + (dtLastDayOfThisWeek.Month != DateTime.Now.Month ? ((dtLastDayOfThisWeek.Day + DateTime.DaysInMonth(dtLastDayOfThisWeek.Year, dtLastDayOfThisWeek.Month)) - DateTime.Now.Day + 1) : (dtLastDayOfThisWeek.Day - DateTime.Now.Day)) + "일";
                    answerBox.Text = "";
                }
                else if (answer.Contains("얼굴"))
                {
                    mainImage.Source = foot;
                }
                else
                {
                    Random random = new Random();
                    int index = random.Next(randomList.Count);
                    mainLabel.Text = randomList[index];
                }
            }
        }
        
        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            faceTimer.Stop();
            if (singing)
            {
                mainImage.Source = jeux;
                singing = false;
            }
            else if (isExitDay)
            {
                mainImage.Source = jeux;
                isExitDay = false;
            }
            else if (isExit)
            {
                mainImage.Source = jeux;
                PlaySoundOffAsync("종료음.mp3");

            }
            else
            {
                mainImage.Source = jeux;
            }
        }

        public void AddLastWordList()
        {
            lastWordList.Add("스칸듐");
            lastWordList.Add("과연소산칼륨");
            lastWordList.Add("고사리무늬");
            lastWordList.Add("장꾼");
        }
    }
}
