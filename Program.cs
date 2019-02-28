using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class Cage<T>   //范型类
    {
        T[] array;
        readonly int Size;
        int num;
        public Cage(int n)
        {
            Size = n;
            num = 0;
            array = new T[Size];
        }
        public void Putin(T pet)
        {
            if (num < Size)
            {
                array[num++] = pet;

            }
            else
            {
                Console.WriteLine("cage is fuul");
            }
            
        }
        public T Takeout()
        {
            if (num > 0)
            {
                return array[--num];
            }
            else
            {
                Console.WriteLine("cage is empty");
                return default(T);
            }
        }
    }

    interface ICatchMice        //接口
    {
        void CatchMice(); 
    }
    interface IClimTree
    {
        void ClimTree();
    }
    abstract public class Pet   //不能实例化
    {

        public Pet(string name)
        {
            _name = name;
            _age = 0;
            
        }
        protected string _name;
        protected int _age;
        public void PrintName()
        {
            Console.WriteLine("pet's name is " + _name);
        }

        public void ShowAge()                  //年龄方法
        {
            Console.WriteLine(_name + "age is " + _age);
        }
        /*virtual*/
        abstract public void Speak();        // virtual 虚方法 abstract 抽象 比虚方法还需 不能掉用
       /* {
            Console.WriteLine("Pet is speeking");
        }*/
      public static Pet operator ++ (Pet pet)     //重载运算符   操作
        {
            ++pet._age;
            return pet;
        }

    }


      public class Dog : Pet,ICatchMice,IClimTree
    {
        public Dog(string name) : base(name)               //派生类构造函数 显示和隐示 this（name）调用当前类的构造函数
        {

        }
       

        new public void PrintName()      //隐藏方法 用基类的成员名称相同的成员来隐藏基类成员
        {
            Console.WriteLine("宠物的名字是" + _name);

        }
       sealed override public void Speak()          //重写 sealed 密闭方法重写（override）    
        {
            Console.WriteLine(_name + " is speeking " + "won won");
        }
        public void CatchMice()
        {
            Console.WriteLine("catch mice");
        }
        public void ClimTree()
        {
            Console.WriteLine("clime tree");
        }
        public static implicit operator Cat(Dog dog)   //显示转换 
        {
            return new Cat(dog._name);
        }
        public void isHappy<T>(T target) //where T : Pet       //范指方法  //约束Pet类
        {
            Console.WriteLine("happy:" + target.ToString());
            //target.PrintName();

        }

        public void WagTail()
        {
            Console.WriteLine(_name + " wang tail");
        }
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>范型接口

    public abstract class DogCmd
    {
        public abstract string GetCmd();
    }
    public class SitDogCmd : DogCmd
    {
        public override string GetCmd()
        {
            return "sit";
        }
    }
    public interface IDogLearn <C> where C: DogCmd  //范型接口
    {
        void Act(C cmd);
    }


    public class Labrador : Dog , IDogLearn<SitDogCmd>
    { 
        public Labrador(string name):base(name)
        {

        }
        public void Act(SitDogCmd cmd)
        {
            Console.WriteLine(cmd.GetCmd());
        }
       

      
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>范型接口

    public class Cat : Pet
    {
        static int Num;
        public delegate void Handler(); //委托
        public static event Handler NewCat; //事件触发
    
        static Cat()
        {
            Num=0;
    
        }
        public Cat(string name) : base(name){
        
            Num++;
            if(NewCat != null)     //事件触发
            {
                NewCat();   
            }
        }
       sealed override public void Speak()
        {
            Console.WriteLine(_name + " is speeking " + "miao miao ");
        }
        static public void ShowNum()
        {
            Console.WriteLine("狗的数量是" + Num);
          
        }
        public static explicit operator Dog(Cat cat) //显示转换
        {
            return new Dog(cat._name);
        }

        public void InnocentLook()
        {
            Console.WriteLine(_name + " InnocentLook");
        }
    }
           class Person { }  //定义一个人的类

    static class PetGuid  //静态类扩展方法
    {
        static public void HowToFeedDog(this Dog dog)  
        {
            Console.WriteLine("play video about how to feed pet");
        }

    }

    class client            //订阅者
    { 

   public void WantADog()
    {
        Console.WriteLine("Greate,I want to see the new cat"); 
    }
    }

    class Program
    {
        delegate void ActCute();  //委托
       

        static void Main(string[] args)
        {                                       /*基类的引用  派生类的对象包含基类部分和派生部分，所以
                                                  我们可以通过一个基类类型的引用指向派生类。
            //Pet dog = new Dog();                  通过指向派生类的基类引用，我们仅仅能访问派生类中的基类部分*/
                                                /*  Dog dog = new Dog();
                                                  dog._name = "jack";
                                                  dog.PrintName();
                                                  //Pet dog = new Dog(); 
                                                  Cat cat = new Cat();
                                             
                 cat._name = "tom";
                                                  cat.PrintName(); */
                                                /*  Pet[] pets = new Pet[]{new Dog("Jack"),new Cat("tom") }; //基类的引用指向派生类 调用的是重写的方法
                                                    for(int i=0; i < pets.Length; i++)
                                                    {
                                                        pets[i].Speak(); 
                                                    }
                                                    Dog c = new Dog("tom2");
                                                    ICatchMice Catch = (ICatchMice)c;
                                                    c.CatchMice();
                                                    Catch.CatchMice();  //只能调用接口内的方法
                                                    IClimTree Clim = (IClimTree)c;
                                                    Clim.ClimTree();
                                                    Cat.ShowNum();
                                                    Dog dog = new Dog("dd");
                                                    dog.HowToFeedDog();
                                                    {
                                                        int i = 3;
                                                        object oi = i;           //装箱操作
                                                        Console.WriteLine("i=" + i + "oi=" + oi.ToString());
                                                        oi = 10;                                                  
                                                        i = 7;
                                                        Console.WriteLine("i=" + i + "oi=" + oi.ToString());
                                                        int j = (int)oi;            //拆箱
                                                       Console.WriteLine("oi= " + j);
                                                  }                                           */


           /*   Dog dog = new Dog("jack");
              dog.Speak();
              Cat cat = dog;       //隐示转换
              cat.Speak();
              cat.PrintName();

              Dog dog2 = (Dog)cat;  //显示转换
              cat.Speak();               */

         /*    Pet[] pets = new Pet[]{new Dog("Jack"),new Cat("tom") }; 
             for(int i=0; i < pets.Length; i++)
             {
                pets[i]++;
                 pets[i].ShowAge();

             }         */
            /*        var catCage = new Cage<Cat>(1);
                    catCage.Putin(new Cat("A"));
                    catCage.Putin(new Cat("b"));
                    var cat = catCage.Takeout();
                    cat.PrintName();          范指类              */

                var dog = new Dog("A");

                    dog.isHappy<Person>(new Person());

                 // dog.isHappy<int>(3);

                // dog.isHappy<Cat>(new Cat("d"));  

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>范型接口

            /*  Labrador dog = new Labrador("A");
              dog.Act(new SitDogCmd());*/

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>范型接口


            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>集合

            /*  List<Dog> list = new List<Dog>();    //lis<>方法
              list.Add(new Dog("A"));
              list.Add(new Dog("B"));
              list.Add(new Dog("C"));
              list.RemoveAt(1);

              for (int i= 0; i < list.Count; ++i)
              {
                  list[i].PrintName();
              }                                */



            /*        Dictionary<string, Dog> dic = new Dictionary<string, Dog>();  //字典
                    dic.Add("a", new Dog("A"));
                    dic.Add("a", new Dog("B"));
                    dic.Add("a", new Dog("C"));
                    dic["A"].PrintName();

                    Stack<Pet> stack = new Stack<Pet>();  //stack 栈
                    stack.Push(new Dog("A"));
                    stack.Push(new Cat("B"));

                    stack.Peek().PrintName();
                    stack.Pop();
                    stack.Peek().PrintName();  */

            /*       Queue<Pet> queue = new Queue<Pet>();     // 队列
                   queue.Enqueue(new Dog("D"));
                   queue.Enqueue(new Dog("E"));
                   queue.Enqueue(new Cat("F"));

                   Pet p = null;
                   p = queue.Dequeue();
                   p.PrintName();
                   p = queue.Dequeue();
                   p.PrintName();
                   p = queue.Dequeue();
                   p.PrintName();            */

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>集合

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>委托 匿名函数lambda表达式

            /*  ActCute del = null;
              Dog dog = new Dog("A");
              Cat cat = new Cat("B");                            
              del = dog.WagTail;
              del+= cat.InnocentLook;
              del += () =>        // lambda表达式
              {
                  Console.WriteLine("do nothing");
              };

              del();       //委托调用 与函数相同 有参数在括号 调用出所有的函数方法 
              
             */

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>委托 匿名函数lambda表达式

        /*    client c1 = new client();
            client c2 = new client();
            Cat.NewCat += c1.WantADog;  //通过对象订阅
           Cat.NewCat += c2.WantADog;
            Cat cat = new Cat("q"); */
      
        }
    }
}
