using System;
using System.Collections.Generic;
using System.Collections;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentCollection FirstCollect = new StudentCollection(); //Liste d'eleve Numero 1
            StudentCollection SecondCollect = new StudentCollection(); //Liste d'eleve Numero 2
            FirstCollect.CollectionName = "Collection numero 1"; //ChangeToRussian
            SecondCollect.CollectionName = "collection numero 2";//ChangeToRussian
            StudentCollection[] FirstArray = { FirstCollect };
            StudentCollection[] SecondArray = { FirstCollect, SecondCollect };
            Journal FirstJournal = new Journal(FirstArray); //Journal Numero 1
            Journal SecondJournal = new Journal(SecondArray); //Journal Numero 2
            Console.WriteLine("-------------------------------------------Etape1");
            FirstCollect.AddDefaults(); //premiere modification sur une liste
            SecondCollect.AddDefaults();
            Student[] StudentToAdd = { new Student("Developpeur c#", 8977, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)), new Student("Developpeur c++", 8077, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)), new Student("Developpeur c", 18977, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)), new Student("Developpeur Javascript", 89770, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)) }; //ChangeToRussian
            FirstCollect.AddStudent(StudentToAdd);
            FirstCollect.Remove(1);
            FirstCollect.Remove(50);
            SecondCollect[0] = new Student("Developpeur Python", 2145, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)); //La reference A ete changer //ChangeToRussian
            SecondCollect.AddStudent(new Student[] { new Student("Developpeur Ruby", 77544, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)) }); //ChangeToRussian
            Console.WriteLine("--------------------------------------------Etape2"); //ChangeToRussian
            Console.WriteLine(FirstJournal.ToString());
            Console.WriteLine(SecondJournal.ToString());
            Console.WriteLine("-----------------------------------Etape 3"); //ChangeToRussian
            Console.WriteLine("Verifions le resume" + FirstCollect.ToString()); //ChangeToRussian
            Console.WriteLine("Verifions le court Resume" + FirstCollect.ToShortString()); //ChangeToRussian
        }
    }
}

public class GetVariables
{
    public static ArrayList getIntArrayList(int Length)
    {
        ArrayList ListToReturn = new ArrayList();
        for (int i = 0; i < Length; i++)
        {
            ListToReturn.Add(new Random().Next(0, 10));
        }
        return ListToReturn;
    }

}
/////////////////////////////////////////////////// Student
public class Student
{
    public Student(string EducationTypeInit, int InformationGroupInit, ArrayList offsetListInit, ArrayList ExamenListInit)
    {
        EducationType = EducationTypeInit;
        InformationGroup = InformationGroupInit;
        offsetList = offsetListInit;
        ExamenList = ExamenListInit;
    }

    public string getInformationStudent()
    {
        return " Education Information: " + EducationType + "| Groupe Information: " + InformationGroup.ToString(); //ChangeToRussian
    }

    public string getExamAndOffsetList()
    {
        string Resume = "| Examen List: "; //ChangeToRussian

        foreach (int item in ExamenList)
        {
            Resume += item.ToString() + " ";
        }

        Resume += "| Offset List: "; //ChangeToRussian

        foreach (int item in offsetList)
        {
            Resume += item.ToString() + " ";
        }

        return Resume + "\n";
    }

    public string getAverageNote()
    {
        int average = 0;
        foreach (int item in ExamenList)
        {
            average = average + item;
        }
        average /= ExamenList.Count;
        return average.ToString();
    }

    public string getInformationGroup()
    {
        return InformationGroup.ToString();
    }

    public string getCount()
    {
        string countOffset = offsetList.Count.ToString();
        string coutExam = ExamenList.Count.ToString();
        return "| Number exam: " + coutExam + "| Number offset: " + countOffset; //ChangeToRussian
    }
    private string EducationType;
    private int InformationGroup;
    private ArrayList offsetList;
    private ArrayList ExamenList;
}
///////////////////////////////////////////// Student List Handler Event
public class StudentListHandlerEventArgs : EventArgs
{
    public string CollectionName { get; set; }
    public string ChangementType { get; set; }
    public Student StudentWhoChange { get; set; }

    public override string ToString()
    {
        return CollectionName + " " + ChangementType + " " + StudentWhoChange.getInformationStudent() + StudentWhoChange.getInformationStudent();
    }
}
///////////////////////////////////////////// Student Collection
public class StudentCollection
{
    public event EventHandler<StudentListHandlerEventArgs> StudentCountChanged;

    public event EventHandler<StudentListHandlerEventArgs> StudentReferenceChanged;
    private List<Student> StudentList = new List<Student>();

    public string CollectionName { get; set; }

    public Student this[int index]
    {
        get
        {
            return StudentList[index];
        }
        set
        {
            StudentList[index] = value;
            if (StudentReferenceChanged != null) // Declenchemant de l'evenement StudentReferenceChanged
            {
                StudentReferenceChanged(this, new StudentListHandlerEventArgs { CollectionName = this.CollectionName, ChangementType = "La reference d'un eleve a ete changer", StudentWhoChange = StudentList[index] }); //ChangeToRussian
            }
        }
    }

    public void AddDefaults()
    {
        Student StudentToAdd = new Student("Programmeur", 102201, GetVariables.getIntArrayList(5), GetVariables.getIntArrayList(5)); //ChangeToRussian
        StudentList.Add(StudentToAdd);
        if (StudentCountChanged != null)
        {
            StudentCountChanged(this, new StudentListHandlerEventArgs { CollectionName = this.CollectionName, ChangementType = "Un nouveau eleve ajouter", StudentWhoChange = StudentToAdd }); //ChangeToRussian
        }
    }

    public void AddStudent(Student[] StudentToAdd)
    {
        for (int i = 0; i < StudentToAdd.Length; i++)
        {
            StudentList.Add(StudentToAdd[i]);
            if (StudentCountChanged != null)
            {
                StudentCountChanged(this, new StudentListHandlerEventArgs { CollectionName = this.CollectionName, ChangementType = "Un nouveau eleve ajouter", StudentWhoChange = StudentToAdd[i] }); //ChangeToRussian
            }
        }
    }

    public bool Remove(int J)
    {
        if (J < 0 || StudentList.Count < J)
        {
            Console.WriteLine("La collection n'existe pas"); //ChangeToRussian
            return false;
        }
        else if (StudentList.Remove(StudentList[J]) && StudentCountChanged != null)
        {
            Console.WriteLine("La collection a bien ete retire"); //ChangeToRussian
            StudentCountChanged(this, new StudentListHandlerEventArgs { CollectionName = this.CollectionName, ChangementType = "Un eleve supprimer", StudentWhoChange = StudentList[J] }); //ChangeToRussian
            return true;
        }
        else
        {
            Console.WriteLine("La collection n'existe pas"); //ChangeToRussian
            return false;
        }
    }

    public override string ToString()
    {
        string Resume = " ";

        for (int i = 0; i < this.StudentList.Count; i++)
        {
            Resume += this.StudentList[i].getInformationStudent() + StudentList[i].getExamAndOffsetList();
        }
        return Resume;
    }

    public string ToShortString()
    {
        string Resume = " ";

        for (int i = 0; i < this.StudentList.Count; i++)
        {
            Resume += this.StudentList[i].getInformationStudent() + "| Average Note: " + StudentList[i].getAverageNote() + StudentList[i].getCount() + "\n"; //ChangeToRussian
        }

        return Resume;
    }
}

//////////////////////////////////////////////Journal Entry

public class JournalEntry
{
    public string CollectionName;
    public string ChangementType;
    public string StudentWhoChange;

    public override string ToString()
    {
        return "| Collection name: " + this.CollectionName + "| Changement Type: " + this.ChangementType + "| Student Information: " + StudentWhoChange + "\n"; //ChangeToRussian
    }
}

/////////////////////////////////////////////////// Journal 
public class Journal
{

    public Journal(StudentCollection[] CollectionToSubscribe)
    {
        for (int i = 0; i < CollectionToSubscribe.Length; i++)
        {
            CollectionToSubscribe[i].StudentCountChanged += StudentCountChangedEventManagement;
            CollectionToSubscribe[i].StudentReferenceChanged += StudentReferenceChangedEventManagement;
        }
    }
    private List<JournalEntry> JournalEntryList = new List<JournalEntry>();

    private void StudentCountChangedEventManagement(object sender, StudentListHandlerEventArgs eventReceived)
    {
        JournalEntryList.Add(new JournalEntry { CollectionName = eventReceived.CollectionName, ChangementType = eventReceived.ChangementType, StudentWhoChange = eventReceived.StudentWhoChange.getInformationGroup() });
    }

    private void StudentReferenceChangedEventManagement(object sender, StudentListHandlerEventArgs eventReceived)
    {
        JournalEntryList.Add(new JournalEntry { CollectionName = eventReceived.CollectionName, ChangementType = eventReceived.ChangementType, StudentWhoChange = eventReceived.StudentWhoChange.getInformationGroup() });
    }

    public override string ToString()
    {
        string JournalResume = " ";
        for (int i = 0; i < this.JournalEntryList.Count; i++)
        {
            JournalResume += this.JournalEntryList[i].ToString();
        }
        return JournalResume;
    }
}