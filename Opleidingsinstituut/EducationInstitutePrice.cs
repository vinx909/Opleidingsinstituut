using System;
using System.Collections.Generic;

namespace Opleidingsinstituut
{
    internal class EducationInstitutePrice
    {
        private const int percentageTotal = 100;
        private const string exceptionClassTypeNotFound = "the given class type was not found";

        private const string classTypeOneName = "written class";
        private const int classTypeOneCostLesson = 50;
        private const int classTypeOneCostStudyMaterial = 50;
        private const string classTypeTwoName = "oral class";
        private const int classTypeTwoCostLesson = 150;
        private const int classTypeTwoCostStudyMaterial = 50;
        private const string classTypeThreeName = "practical class";
        private const int classTypeThreeCostLesson = 150;
        private const int classTypeThreeCostStudyMaterial = 125;

        private const int ageCostReductionThreshold = 19;
        private const int ageCostReductionPercentageReduction = 2;

        private static List<ClassType> classTypes;

        internal static string[] GetClassTypes()
        {
            SetupClassTypes();
            string[] toReturn = new string[classTypes.Count];
            for(int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = classTypes[i].GetName();
            }
            return toReturn;
        }

        internal static double GetPrice(string[] classesToFollow, int studentBirthDateDay, int studentBirthDateMonth, int studentBirthDateYear, int currentDateDay, int currentDateMonth, int currentDateYear, bool employmentAgencyMediated)
        {
            SetupClassTypes();
            int[] amountOfEachClassToFollow = new int[classTypes.Count];
            for (int i = 0; i < amountOfEachClassToFollow.Length; i++)
            {
                amountOfEachClassToFollow[i] = 0;
            }
            foreach (string classToFollow in classesToFollow)
            {
                bool classTypeFound = false;
                for(int i=0;i< amountOfEachClassToFollow.Length; i++)
                {
                    if (classTypes[i].IsSameName(classToFollow))
                    {
                        classTypeFound = true;
                        amountOfEachClassToFollow[i]++;
                        break;
                    }
                }
                if (classTypeFound == false)
                {
                    throw new Exception(exceptionClassTypeNotFound);
                }
            }
            double costLessons = 0;
            for (int i = 0; i < amountOfEachClassToFollow.Length; i++)
            {
                costLessons += amountOfEachClassToFollow[i]* classTypes[i].GetCostLesson();
            }
            if ((studentBirthDateYear + ageCostReductionThreshold > currentDateYear)
                || (studentBirthDateYear + ageCostReductionThreshold == currentDateYear && studentBirthDateMonth > currentDateMonth)
                || (studentBirthDateYear + ageCostReductionThreshold == currentDateYear && studentBirthDateMonth == currentDateMonth && studentBirthDateYear > currentDateDay))
            {
                costLessons *= 1.0 / percentageTotal * (percentageTotal - ageCostReductionPercentageReduction);
            }
            double costMaterials = 0;
            if (employmentAgencyMediated == false)
            {
                for (int i = 0; i < amountOfEachClassToFollow.Length; i++)
                {
                    costMaterials += amountOfEachClassToFollow[i] * classTypes[i].GetCostStudyMaterial();
                }
            }
            return costLessons + costMaterials;
        }

        private static void SetupClassTypes()
        {
            if(classTypes == null)
            {
                classTypes = new List<ClassType>();
                classTypes.Add(new ClassType(classTypeOneName, classTypeOneCostLesson, classTypeOneCostStudyMaterial));
                classTypes.Add(new ClassType(classTypeTwoName, classTypeTwoCostLesson, classTypeTwoCostStudyMaterial));
                classTypes.Add(new ClassType(classTypeThreeName, classTypeThreeCostLesson, classTypeThreeCostStudyMaterial));
            }
        }

        private class ClassType
        {
            private string name;
            private int costLesson;
            private int costStudyMaterial;

            internal ClassType(string name, int costLesson, int costStudyMaterial)
            {
                this.name = name;
                this.costLesson = costLesson;
                this.costStudyMaterial = costStudyMaterial;
            }
            internal string GetName()
            {
                return name;
            }
            internal bool IsSameName(string name)
            {
                return this.name.Equals(name);
            }
            internal int GetCostLesson()
            {
                return costLesson;
            }
            internal int GetCostStudyMaterial()
            {
                return costStudyMaterial;
            }
        }
    }
}