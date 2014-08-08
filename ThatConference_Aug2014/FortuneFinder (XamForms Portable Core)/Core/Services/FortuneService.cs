using System;
using System.Linq;

namespace FortuneFinder.Core
{
    public class FortuneService : IFortuneService
    {
        Fortunes AllFortunes { get; set; }

        public FortuneService()
        {
            var fortuneStrings = Data.Split('\n');
            AllFortunes = new Fortunes(fortuneStrings);
        }

        #region IFortuneService implementation

        public Fortunes GetFortunes(string searchText)
        {
            var result = AllFortunes.Where(s => s.Contains(searchText)).ToArray();
            return new Fortunes(result);
        }

        #endregion

        string Data = 
@"You will soon embark on a business venture.
You believe in the goodness of man kind.
You will have a long and wealthy life.
You will take a pleasant journey to a place far away.
You are a person of culture.
Keep it simple. The more you say, the less people remember.
Life is like a dogsled team. If you ain't the lead dog, the scenery never changes.
Prosperity makes friends and adversity tries them.
Nothing seems impossible to you.
Patience is bitter, but its fruit is sweet.
The only certainty is that nothing is certain.
Success is the sum of my unique visions realized by the sweat of perseverance.
When you expect your opponent to yield, you also should avoid hurting him.
Human evolution: “wider freeway but narrower viewpoints.
Intelligence is the door to freedom and alert attention is the mother of intelligence.
Back away from individuals who are impulsive.
Enjoyed the meal? Buy one to go too.
You believe in the goodness of mankind.
A big fortune will descend upon you this year. 
Now these three remain, faith, hope, and love. The greatest of these is love.
For success today look first to yourself.
Determination is the wake-up call to the human will.
There are no limitations to the mind except those we aknowledge.
A merry heart does good like a medicine.
Whenever possible, keep it simple.
Your dearest wish will come true. 
Poverty is no disgrace.
If you don’t do it excellently, don’t do it at all.
You have an unusual equipment for success, use it properly.
Emotion is energy in motion.
You will soon be honored by someone you respect.
Punctuality is the politeness of kings and the duty of gentle people everywhere.
Your happiness is intertwined with your outlook on life.
Elegant surroundings will soon be yours.
If you feel you are right, stand firmly by your convictions.
Your smile brings happiness to everyone you meet. 
Instead of worrying and agonizing, move ahead constructively.
Do you believe? Endurance and persistence will be rewarded. 
A new business venture is on the horizon.
Never underestimate the power of the human touch.
Hold on to the past but eventually, let the times go and keep the memories into the present.
Truth is an unpopular subject. Because it is unquestionably correct.
The most important thing in communication is to hear what isn’t being said.
You are broad minded and socially active.
Your dearest dream is coming true. God looks after you especially. 
You will recieve some high prize or award.
Your present question marks are going to succeed.
You have a fine capacity for the enjoyment of life.
You will live long and enjoy life.
An admirer is concealing his/her affection for you.
The greatest risk is not taking one.
Now is the time to try something new.
If winter comes, can spring be far behind?
A stranger, is a friend you have not spoken to yet.
Conquer your fears or they will conquer you.
You learn from your mistakes, you will learn a lot today.
You only need look to your own reflection for inspiration. Because you are Beautiful!
You are not judged by your efforts you put in; you are judged on your performance.
Rivers need springs.
Good news from afar may bring you a welcome visitor.
When all else seems to fail, smile for today and just love someone.
When you look down, all you see is dirt, so keep looking up.
If you are afraid to shake the dice, you will never throw a six.
A single conversation with a wise man is better than ten years of study.
Happiness is often a rebound from hard work. 
The world may be your oyster, but that doesn't mean you'll get it's pearl.
Your life will be filled with magical moments.
You're true love will show himself to you under the moonlight. 
Do not follow where the path may lead. Go where there is no path...and leave a trail
Do not fear what you don't know
The object of your desire comes closer.
You have a flair for adding a fanciful dimension to any story.
If you wish to know the mind of a man, listen to his words
The most useless energy is trying to change what and who God so carefully created.
Do not be covered in sadness or be fooled in happiness they both must exist
You will have unexpected great good luck.
You will have a pleasant surprise
All progress occurs because people dare to be different.
Your ability for accomplishment will be followed by success.
The world is always ready to receive talent with open arms.
Things may come to those who wait, but only the things left by those who hustle.
We can't help everyone. But everyone can help someone.
Every day is a new day. But tomorrow is never promised.
Express yourself: Don't hold back!
It is not necessary to show others you have change; the change will be obvious.
You have a deep appreciation of the arts and music.
If your desires are not extravagant, they will be rewarded.
You try hard, never to fail. You don't, never to win.
Never give up on someone that you don't go a day without thinking about.
It never pays to kick a skunk.
In case of fire, keep calm, pay bill and run.
Next full moon brings an enchanting evening.
Not all closed eye is sleeping nor open eye is seeing.
Impossible is a word only to be found in the dictionary of fools.
You will soon witness a miracle.
The time is alway right to do what is right.
Love is as necessary to human beings as food and shelter.
You will make heads turn.
You are extremely loved. Don't worry :)
If you are never patient, you will never get anything done. If you believe you can do it, you will be rewarded with success.
A wish is what makes life happen when you dream of rose petals.
Love can turn cottage into a golden palace. 
Lend your money and lose your freind.
You will kiss your crush ohhh lalahh
You will be rewarded for being a good listener in the next week.
If you never give up on love, It will never give up on you. 
Unleash your life force. 
Your wish will come true.
There is a prospect of a thrilling time ahead for you.
No distance is too far, if two hearts are tied together. 
Land is always in the mind of the flying birds.
Try? No! Do or do not, there is no try.
Do not worry, you will have great peace.
It's about time you asked that special someone on a date.
You create your own stage ... the audience is waiting.
It is never too late. Just as it is never too early.
Discover the power within yourself. 
Good things take time. 
Stop thinking about the road not taken and pave over the one you did.
Put your unhappiness aside. Life is beautiful, be happy.
You can still love what you can not have in life.
Make a wise choice everyday.
Circumstance does not make the man; it reveals him to himself.
The man who waits till tomorrow, misses the opportunities of today.
Life does not get better by chance. It gets better by change.
If you never expect anything you can never be disappointed.
People in your surroundings will be more cooperative than usual.
True wisdom is found in happiness.
Ones always regrets what could have done. Remember for next time.
Follow your bliss and the Universe will open doors where there were once only walls.
Find a peaceful place where you can make plans for the future. 
All the water in the world can't sink a ship unless it gets inside.
The earth is a school learn in it.
In music, one must think with his heart and feel with his brain.
If you speak honestly, everyone will listen.
Ganerosity will repay itself sooner than you imagine.
good things take time
Do what is right, not what you should. 
To effect the quality of the day is no small achievement.
Simplicity and clearity should be the theme in your dress.
Virtuous find joy while Wrongdoers find grieve in their actions.
Not all closed eye is sleeping, nor open eye is seeing. 
Bread today is better than cake tomorrow.
In evrything there is a piece of truth.But a piece.
A feeling is an idea with roots.
Man is born to live and not prepare to live 
It's all right to have butterflies in your stomach. Just get them to fly in formation. 
If you don t give something, you will not get anything
The harder you try to not be like your parents, the more likely you will become them
Someday everything will all make perfect sense
you will think for yourself when you stop letting others think for you
Everything will be ok. Don't obsess. Time will prove you right, you must stay where you are.
Let's finish this up now, someone is waiting for you on that
The finest men like the finest steels have been tempered in the hottest furnace.
A dream you have will come true 
The worst of friends may become the best of enemies, but you will always find yourself hanging on. 
I think, you ate your fortune while you were eating your cookie
If u love someone keep fighting for them 
Do what you want, when you want, and you will be rewarded
Let your fantasies unwind... 
The cooler you think you are the dumber you look
Expect great things and great things will come 
The Wheel of Good Fortune is finally turning in your direction!
Don't lead if you won't lead.
You will always be successful in your professional career 
Share your hapiness with others today.
It's up to you to clearify.
Your future will be happy and productive.
Seize every second of your life and savor it.
Those who walk in other's tracks leave no footprints.
Failure is the mother of all success. 
Difficulty at the beginning useually means ease at the end.
Do not seek so much to find the answer as much as to understand the question better. 
Your way of doing what other people do their way is what makes you special.
A beautiful, smart, and loving person will be coming into your life. 
Friendship is an ocean that you cannot see bottom.
Your life does not get better by chance, it gets better by change. 
Our duty,as men and women,is to proceed as if limits to our ability did not exist.
A pleasant expeience is ahead:don't pass it by. 
Our perception and attitude toward any situation will determine the outcome
They say you are stubborn; you call it persistence.
Two small jumps are sometimes better than one big leap.
A new wardrobe brings great joy and change to your life.
The cure for grief is motion.
It's a good thing that life is not as serious as it seems to the waiter
I hear and I forget. I see and I remember. I do and I understand.
I have a dream....Time to go to bed.
Ideas you believe are absurd ultimately lead to success! 
A human being is a deciding being.
Today is an ideal time to water your parsonal garden.
Some men dream of fortunes, others dream of cookies.
Things are never quite the way they seem.
the project on your mind will soon gain momentum
YOUR FAILURES WILL LEAD YOU TO YOUR SUCCESS.
IN ORDER TO GET THE RAINBOW, YOU MUST ENDURE THE RAIN. 
Beauty is simply beauty. originality is magical.
Your dream will come true when you least expect it.
Let not your hand be stretched out to receive and shut when you should repay.
Don't worry, half the people you know are below average. 
Vision is the art of seeing what is invisible to others.";
    }
}

