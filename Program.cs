using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator
{
    class Program
    {

        //***************************************************************
        //  Title:          The Budget Calculator
        //
        //  App. Type:      Console
        //
        //  Description:    An application that gives users the option
        //                  to determine how much they are spending in
        //                  a specified time period.
        //
        //  Author:         Colin Haroutunian
        //
        //  Date Created:   April 24th, 2020
        //  Last Modified:  April 26th, 2020
        //***************************************************************

        static void Main(string[] args)
        {
            Greeting();
            BudgetCreator();
            Outro();
        }


        #region GREETING, OUTRO
        /// <summary>
        /// 
        ///     GREETING OPERATIONS
        /// 
        /// </summary>
        static void Greeting()
        {
            Console.CursorVisible = false;
            Title();
            GreetingMessage();
            SingleConfirmation();
            Console.Clear();
        }
        static void GreetingMessage()
        {
            Console.WriteLine("Welcome to the budget calculator.");
            Console.WriteLine("With this service, you can make an effective budget to balance out your expenses.");
            Console.WriteLine("The application covers multiple factors, with some being: education, income, and travel.");
            Console.WriteLine();
            Console.WriteLine("None of the information requested is particularly sensitive.");
            Console.WriteLine("However, if you feel uncomfortable answering questions, do not answer.");
            Console.WriteLine("Do note that not answering questions, however, may affect the calculator's effectivity for you.");
            Console.WriteLine();
        }
        static void Title()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The Budget Calculator");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// 
        ///     EXIT OPERATIONS
        /// 
        /// </summary>
        static void Outro()
        {
            Title();
            Console.WriteLine("Thank you for using the Budget Calculator!");
            Console.WriteLine();
            SingleConfirmation();
        }
        #endregion


        #region BUDGET CREATION
        /// <summary>
        /// 
        ///     BUDGET CREATION PROCESS, VIEWER
        ///     
        /// </summary>
        static void BudgetCreator()
        {
            Console.Clear();
            CreatorEntryText();
            bool userEntry = DualConfirmation();
            
            if (userEntry == true)
            {
                Console.Clear();
                int varTime = TimelineQuery();
                Console.Clear();
                double[] factValue = FactorsCases(varTime);
                Console.Clear();
                BudgetViewer(factValue);
                SingleConfirmation();
            }
            Console.Clear();
        }
        static void CreatorEntryText()
        {
            Console.WriteLine("You have now entered the budget creator process.");
            Console.WriteLine("Once you begin entering information, you cannot exit the application.");
            Console.WriteLine("Otherwise, the information will not be available for future reference.");
            Console.WriteLine();
            Console.WriteLine("Are you sure that you want to enter?");
            Console.WriteLine();
        }
        static void BudgetViewer(double[] factorVal)
        {
            string[] factorNames = { "Travel", "Food", "Education", "Insurance", "Housing", "Savings", "Loans", "Health", "Amenities", "Income", "Other" };
            Title();
            Console.WriteLine();
            int position = 0;
            int listpos = 1;
            double total = 0;
            foreach (double factor in factorVal)
            {
                Console.WriteLine("\t{0}) {1}: {2}", listpos, factorNames[position], factorVal[position].ToString("C"));
                total = total + factorVal[position];
                position++;
                listpos++;
            }
            string quality;
            if (total < 0)
                quality = "in a deficit";
            else
                quality = "as a positive";
            Console.WriteLine();
            Console.WriteLine("\tTotal: {0} {1}", total.ToString("C"), quality);
            Console.WriteLine();
            Console.WriteLine("Please write down or screenshot your total, as it will NOT be saved.");
            Console.WriteLine();
        }


        /// <summary>
        /// 
        ///     TIMELINE ENTRY, w/ PRESET & CUSTOM  
        /// 
        /// </summary>
        static int TimelineQuery()
        {
            TimelineText();

            int varTime = 0;
            bool timeChosen = false;

            do
            {
                ConsoleKeyInfo infoKey = Console.ReadKey(intercept: true);
                switch (infoKey.Key)
                {
                    case ConsoleKey.D1:
                        varTime = 1;
                        timeChosen = true;
                        break;
                    case ConsoleKey.D2:
                        varTime = 7;
                        timeChosen = true;
                        break;
                    case ConsoleKey.D3:
                        varTime = 30;
                        timeChosen = true;
                        break;
                    case ConsoleKey.D4:
                        varTime = 365;
                        timeChosen = true;
                        break;
                    case ConsoleKey.D5:
                        varTime = CustomTimeQuery();
                        timeChosen = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect '{0}' key entered.", infoKey.Key);
                        Console.WriteLine("Retry your key submission.");
                        break;
                }
            } while (timeChosen != true);

            return varTime;
        }
        static void TimelineText()
        {
            Console.WriteLine("For the calculator to work correctly, you must choose a timeline to work off of.");
            Console.WriteLine("There are five options: daily, weekly, monthly, yearly, and a custom-set time.");
            Console.WriteLine("Please choose your desired timeline from those listed below.");
            Console.WriteLine();
            Console.WriteLine("1) Daily");
            Console.WriteLine("2) Weekly");
            Console.WriteLine("3) Monthly");
            Console.WriteLine("4) Yearly");
            Console.WriteLine("5) Custom-set");
        }
        static int CustomTimeQuery()
        {
            Console.CursorVisible = true;
            Console.WriteLine();
            CustomTimeText();
            bool goodRes = false;
            int userTime;
            string userRes;
            do
            {
                userRes = Console.ReadLine();
                goodRes = int.TryParse(userRes, out userTime);
                if (goodRes == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: incorrect '{0}' value given.", userRes);
                    Console.Write("Try again, but with only number values: ");
                }
            } while (goodRes == false);
            return userTime;
        }
        static void CustomTimeText()
        {
            Console.WriteLine("For a custom time, you will want to enter in a number of days you would like to budget on.");
            Console.WriteLine("When entering in a value, do NOT provide units in the submission.");
            Console.WriteLine();
            Console.Write("Please enter your time here: ");
        }


        /// <summary>
        /// 
        ///     FACTOR MENU LIST
        /// 
        /// </summary>
        static double[] FactorsCases(int time)
        {
            double[] factValue = new double[11];
            bool[] editQuality = new bool[11];

            bool submitBudget = false;

            do
            {
                Console.CursorVisible = true;
                FactorsIntro();
                FactorsList(editQuality);

                Console.WriteLine();
                Console.Write("Please enter the corresponding number here: ");
                string factor = Console.ReadLine();
                switch (factor)
                {
                    case "1":
                        (factValue[0], editQuality[0]) = Expenses("Travel", time);
                        break;
                    case "2":
                        (factValue[1], editQuality[1]) = Expenses("Food", time);
                        break;
                    case "3":
                        (factValue[2], editQuality[2]) = Education(time);
                        break;
                    case "4":
                        (factValue[3], editQuality[3]) = Expenses("Insurance", time);
                        break;
                    case "5":
                        (factValue[4], editQuality[4]) = Expenses("Housing", time);
                        break;
                    case "6":
                        (factValue[5], editQuality[5]) = Expenses("Savings", time);
                        break;
                    case "7":
                        (factValue[6], editQuality[6]) = Expenses("Loans", time);
                        break;
                    case "8":
                        (factValue[7], editQuality[7]) = Expenses("Health", time);
                        break;
                    case "9":
                        (factValue[8], editQuality[8]) = Expenses("Amenities", time);
                        break;
                    case "10":
                        (factValue[9], editQuality[9]) = Income(time);
                        break;
                    case "11":
                        (factValue[10], editQuality[10]) = Expenses("Other", time);
                        break;
                    case "12":
                        submitBudget = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect '{0}' was entered.", factor);
                        Console.Write("Retry your submission with values '1-12': ");
                        SingleConfirmation();
                        break;
                }
                Console.Clear();
            } while (submitBudget == false);

            return factValue;
        }
        static void FactorsIntro()
        {
            Console.WriteLine("Now that you have a timeline chosen, you can begin to build your budget.");
            Console.WriteLine("You can do this by selecting one or many factors and providing financial information about them.");
            Console.WriteLine("It is suggested that you have receipts or documentation on the factors you select in order to expedite the data entry.");
            Console.WriteLine();
        }
        static void FactorsList(bool[] editQuality)
        {
            string[] factorNames = { "Travel", "Food", "Education", "Insurance", "Housing", "Savings", "Loans", "Health", "Amenities", "Income", "Other" };

            int position = 0;
            int listVal = 1;

            foreach (string x in factorNames)
            {
                if (editQuality[position] == true)
                    Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("{0}) {1}", listVal, factorNames[position]);
                
                position++;
                listVal++;

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("12) SUBMIT BUDGET");
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// 
        ///     MULTIPLE USE FACTOR PROCESSING METHODS
        /// 
        /// </summary>
        static (double, bool) Expenses(string factorName, int time)
        {
            Console.Clear();
            double expense = 0;
            bool altered = false;
            bool receipt = Documentation(factorName);
            Console.WriteLine();
            Console.WriteLine("---");
            Console.WriteLine();
            if (receipt == true)
            {
                Console.WriteLine("Enter in values from the given time period: {0} days.", time);
                Console.WriteLine();
                expense = (SumRepeat(time)) * -1;
                altered = true;
            }
            else
            {
                if (factorName == "Loans" || factorName == "Insurance")
                {
                    Obstacle(factorName);
                    altered = false;
                }
                else
                {
                    NoReceipt(factorName);

                    bool userChoice = DualConfirmation();

                    if (userChoice == true)
                    {
                        if (factorName == "Travel")
                            expense = TravelExp(time);
                        if (factorName == "Food" || factorName == "Amenities" || factorName == "Other")
                            expense = GenericEstimate(factorName, time);
                        if (factorName == "Savings")
                            expense = SaveFundsExp(time);
                        if (factorName == "Health")
                            expense = HealthExp();
                        altered = true;
                    }
                }
            }
            return (expense, altered);
        }
        static void Obstacle(string factorName)
        {
            Console.WriteLine("Unfortunately, {0} payments cannot be processed without documentation.", factorName);
            Console.WriteLine("Please find forms or receipts relating to this factor to perform a calculation.");
            Console.WriteLine();
        }
        static bool Documentation(string factorName)
        {
            Console.WriteLine("Do you have documentation - by way of receipts, or other methods - for your {0} expenses?", factorName.ToUpper());
            bool receiptQ = DualConfirmation();
            return receiptQ;
        }
        static double SumRepeat(int time)
        {
            bool userContinue = false;
            double valEntry;
            double sum = 0;

            do
            {
                Console.WriteLine("It is acceptable to enter values with decimals.");
                Console.WriteLine("When finished entering values, press ESC when prompted.");
                Console.WriteLine();
                Console.Write("Enter a new value here: ");

                bool goodValue = false;

                do
                {
                    string userEntry = Console.ReadLine();
                    goodValue = double.TryParse(userEntry, out valEntry);
                    if (goodValue == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect '{0}' was entered.", userEntry);
                        Console.Write("Retry your submission with only number values: ");
                    }
                } while (goodValue == false);

                sum = sum + valEntry;

                Console.WriteLine();
                Console.WriteLine("Would you like to continue adding values?");
                userContinue = DualConfirmation();
                Console.WriteLine();

            } while (userContinue == true);

            return sum;
        }
        static double UserValues(string valueEntered)
        {
            bool valueDone = false;
            double entry;

            do
            {
                Console.WriteLine("Please avoid the use of units in your entry.");
                Console.Write("Please enter your {0} here: ", valueEntered.ToUpper());
                valueDone = double.TryParse(Console.ReadLine(), out entry);
                if (valueDone == false)
                {
                    Console.WriteLine("ERROR: incorrect entry.");
                    Console.WriteLine("Try again. Do not use units.");
                }
            } while (valueDone == false);

            return entry;
        }
        static void NoReceipt(string factorName)
        {
            Console.WriteLine("Without receipts for {0} expenses, the process will require more time to complete.", factorName.ToUpper());
            Console.WriteLine("Estimates can be made instead through different methods.");
            Console.WriteLine("Would you still like to make a calculation for this?");
            Console.WriteLine();
        }


        /// <summary>
        /// 
        ///     GENERIC (food, amenities, other) EXPENSE PROCESSOR
        /// 
        /// </summary>
        static double GenericEstimate(string factorName, int time)
        {
            EstText(factorName);
            SingleConfirmation();
            Console.WriteLine();
            Console.WriteLine("Enter in values from the given time period: {0} days.", time);
            double estCost = (SumRepeat(time)) * -1;
            return estCost;
        }
        static void EstText(string factor)
        {
            Console.Clear();
            Console.WriteLine("Without a receipt, it is still possible to estimate your one-time purchases for {0}.", factor);
            Console.WriteLine("To find these values, you can: ");
            Console.WriteLine("\t1) Search the retailer's web stores for prices.");
            Console.WriteLine("\t2) View the store's paper advertisements.");
            Console.WriteLine("\t\tNOTE: items in advertisements could be on sale, which may result incorrect estimates.");
            Console.WriteLine();
        }


        /// <summary>
        /// 
        ///     PERSONAL SAVINGS FUNDS PROCESSOR
        /// 
        /// </summary>
        static void SavingsInitial(int time)
        {
            Console.WriteLine("In the time period of {0} days, you may have contributed funding towards: ", time);
            Console.WriteLine("\t1) A personal or joint savings account,");
            Console.WriteLine("\t2) A retirement fund,");
            Console.WriteLine("\t3) A college fund.");
            Console.WriteLine();
            Console.WriteLine("Here, you will be able to enter values for each of these funds.");
            Console.WriteLine("If you have not contributed a value to one of them, simply enter a '0' value.");
            Console.WriteLine();
        }
        static double SaveFundsExp(int time)
        {
            SavingsInitial(time);
            double accSavings = UserValues("Savings");
            double accRetirement = UserValues("Retirement");
            double accCollege = UserValues("College");
            double funding = (accSavings + accRetirement + accCollege) * -1;
            return funding;
        }


        /// <summary>
        /// 
        ///     HEALTH (medicine, hospital visits) EXPENSE PROCESSOR
        /// 
        /// </summary>
        static void HealthIntro()
        {
            Console.Clear();
            Console.WriteLine("Unfortunately, certain medical expenses cannot be adequately processed without receipts.");
            Console.WriteLine("However, cleaning products and generic products can be by searching online.");
            Console.WriteLine("For those without quality processing, it is suggested that you estimate how much they may cost. These include: ");
            Console.WriteLine("\t1) Billing for hospital visits,");
            Console.WriteLine("\t2) Medication unavailable at stores,");
            Console.WriteLine();
        }
        static double HealthExp()
        {
            HealthIntro();
            SingleConfirmation();
            double firstAid = UserValues("First Aid - Bandages, etc.");
            Console.WriteLine();
            double cleaning = UserValues("Cleaning Products - Soap, etc.");
            Console.WriteLine();
            double medication = UserValues("Medication");
            Console.WriteLine();
            double hosVisit = UserValues("Hospital Visits");
            Console.WriteLine();
            double expense = (firstAid + cleaning + medication + hosVisit) * -1;
            return expense;
        }

        /// <summary>
        /// 
        ///     TRAVEL EXPENSE PROCESSOR
        /// 
        /// </summary>
        static double TravelExp(int time)
        {
            TravelMsgInitial();
            SingleConfirmation();
            double travelSum = (TravelEvents(time)) * -1;
            return travelSum;
        }
        static void TravelMsgInitial()
        {
            Console.WriteLine("Without receipts, you will need to make an estimate of your travel costs.");
            Console.WriteLine("While flight and train costs cannot be estimated here, travel through cars can be.");
            Console.WriteLine("NOTE: the value will not be exact, since gasoline prices vary, and speeds while driving also vary.");
            Console.WriteLine();
        }
        static void TravelMsgHandler(int time)
        {
            Console.WriteLine("Calculating travel expenses for {0} days will be a multi-step process: ", time);
            Console.WriteLine("\t1) Find the miles per gallon (MPG) of the car used.");
            Console.WriteLine("\t2) Use an online map service - such as Google Maps or Bing Maps - to estimate the miles driven.");
            Console.WriteLine("\t\tThe distance should be calculated as 'to-and-from' or 'there-and-back' of the destinations.");
            Console.WriteLine("\t3) Using your preferred website, estimate the cost of gasoline per gallon in your area.");
            Console.WriteLine("\t\tHere is a suggested resource: https://gasprices.aaa.com/state-gas-price-averages/");
            Console.WriteLine();
            Console.WriteLine("NOTE: if you use multiple vehicles or destinations, you will be able to have a separate entry for these.");
            Console.WriteLine("\tDo NOT combine these travels together into one.");
            Console.WriteLine();
        }
        static void TravelAddScenario(double travelSum)
        {
            Console.WriteLine("Your current travel cost is: ${0}.", travelSum);
            Console.WriteLine("Would you like to continue adding new travels?");
            Console.WriteLine();
        }
        static double TravelEvents(int time)
        {
            bool userRes = true;
            double travelSum = 0;

            do
            {
                TravelMsgHandler(time);
                double indivSum;
                double mpg = UserValues("car's MPG");
                double distance = UserValues("distance traveled");
                double price = UserValues("gas price");
                indivSum = ((distance / mpg) * price) * time;
                travelSum = travelSum + indivSum;
                TravelAddScenario(travelSum);
                userRes = DualConfirmation();
            } while (userRes == true);
            return travelSum;
        }

        /// <summary>
        /// 
        ///     EDUCATION (supplies, grants) EXPENSE PROCESSOR
        /// 
        /// </summary>
        static (double, bool) Education(int time)
        {
            double finAid = 0;
            double necessities = 0;
            double tuitionVal = 0;

            double finalExp = 0;
            bool edited = false;
            bool exit = false;

            do
            {
                MsgEducation();
                ConsoleKeyInfo userKey = Console.ReadKey(intercept: true);
                switch (userKey.Key)
                {
                    case ConsoleKey.D1:
                        finAid = FinancialAid(time);
                        break;
                    case ConsoleKey.D2:
                        necessities = SchoolNecessities(time);
                        break;
                    case ConsoleKey.D3:
                        tuitionVal = TuitionCost(time);
                        break;
                    case ConsoleKey.Enter:
                        edited = true;
                        exit = true;
                        break;
                    case ConsoleKey.Escape:
                        edited = false;
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect '{0}' key entered.", userKey.Key);
                        Console.WriteLine("Retry your key submission.");
                        break;
                }
            } while (exit == false);

            finalExp = finAid - (necessities + tuitionVal);

            return (finalExp, edited);
        }
        static double FinancialAid(int time)
        {
            double aidVal;
            Console.WriteLine("For financial aid, you will be entering in grants, scholarships, and loans, respectively.");
            Console.WriteLine("If for some reason you do not have exact values, try estimating them.");
            Console.WriteLine();
            aidVal = SumRepeat(time);
            return aidVal;
        }
        static double SchoolNecessities(int time)
        {
            double supplies;
            Console.WriteLine("For school necessities, you can enter in as many supplies individually as you would like.");
            Console.WriteLine("Supplies can be considered: textbooks, writing utencils, computers, accessories, folders, etc.");
            Console.WriteLine("If for some reason you do not have exact values, try searching for them online through your bookstore.");
            Console.WriteLine();
            supplies = SumRepeat(time);
            return supplies;
        }
        static double TuitionCost(int time)
        {
            double userTuition;
            bool precalc;
            Console.WriteLine("For the tuition, you will need exact values.");
            Console.WriteLine("Do you have the cost already calculated?");
            precalc = DualConfirmation();
            if (precalc == true)
            {
                userTuition = UserValues("tuition cost");
            }
            else
            {
                userTuition = AltTuitionCost();
            }
            return userTuition;
        }
        static double AltTuitionCost()
        {
            Console.WriteLine("You will first need to find your college's tuition cost, per credit hour, for your status - in-state, out-of-state, etc.");
            double credCost = UserValues("cost per credit hour");
            Console.WriteLine("Now, you will need to enter in the amount of credits that you are currently taking.");
            double userCredits = UserValues("credits taken");
            double userTuition = credCost * userCredits;
            return userTuition;
        }
        static void MsgEducation()
        {
            Console.Clear();
            Console.WriteLine("For education, there are numerous factors that can alter the final cost.");
            Console.WriteLine("There are three main categories to this. Once all have been adjusted, press ENTER at this menu.");
            Console.WriteLine("NOTE: if you want to leave without submitting, press ESC. However, you will lose ALL information entered here.");
            Console.WriteLine("\t1) Financial aid - grants, scholarship, student loans");
            Console.WriteLine("\t2) Necessities - textbooks, writing supplies, paper, etc.");
            Console.WriteLine("\t3) Tuition costs - dollars per credit hour");
            Console.WriteLine("\tENTER) Submit completed factors");
            Console.WriteLine("\tESC) Exit without submitting");
            Console.WriteLine();
        }


        /// <summary>
        /// 
        ///     BASIC INCOME INFORMATION
        /// 
        /// </summary>
        static (double, bool) Income(int time)
        {
            Console.Clear();
            IncomeIntro(time);
            double userIncome = 0;
            bool completed = false;

            do
            {
                ConsoleKeyInfo userRes = Console.ReadKey(intercept: true);
                switch (userRes.Key)
                {
                    case ConsoleKey.D1:
                        userIncome = UserValues("yearly wage");
                        completed = true;
                        break;
                    case ConsoleKey.D2:
                        userIncome = OtherWage(time);
                        completed = true;
                        break;
                    case ConsoleKey.D3:
                        userIncome = 0;
                        completed = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect key '{0}' was entered.", userRes.Key);
                        Console.WriteLine("Retry your submission with keys: 1, 2, 3.");
                        break;
                }
            } while (completed == false);

            return (userIncome, completed);
        }
        static double OtherWage(int time)
        {
            OtherWageMsg();
            SingleConfirmation();

            Console.WriteLine();
            Console.WriteLine("---");
            Console.WriteLine();
            Console.WriteLine("Now that you have your estimated yearly wage, it will now be processed through a series of functions.");

            double annual = UserValues("annual wage");
            double daily = annual / 365;
            double earnings = daily * time;

            return earnings;
        }
        static void OtherWageMsg()
        {
            Console.WriteLine();
            Console.WriteLine("---");
            Console.WriteLine();
            Console.WriteLine("Without a known wage, you will have to estimate your current wage.");
            Console.WriteLine("Please use this resource to estimate your job's yearly wage:");
            Console.WriteLine("\thttps://www.bls.gov/bls/blswage.htm");
            Console.WriteLine();
        }
        static void IncomeIntro(int time)
        {
            Console.WriteLine("For the time period of {0} days, you will need to enter in your overall income.", time);
            Console.WriteLine("\tNOTE: taxes are not calculated during the process.");
            Console.WriteLine("There are three options to choose from for income: ");
            Console.WriteLine("\t1) Known wage");
            Console.WriteLine("\t2) Unsure / Other");
            Console.WriteLine("\t3) No income");
        }
        #endregion


        #region CONFIRMATIONS
        static void SingleConfirmation()
        {
            Console.WriteLine("Press ENTER once you have finished reading.");
            ConsoleKeyInfo keyEntry;
            bool validKey = false;
            do
            {
                keyEntry = Console.ReadKey(intercept: true);
                switch (keyEntry.Key)
                {
                    case ConsoleKey.Enter:
                        validKey = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect '{0}' key entered.", keyEntry.Key);
                        Console.WriteLine("Retry your key submission.");
                        break;
                }
            } while (validKey != true);
            Console.WriteLine("---");
        }


        /// <summary>
        /// 
        /// The method below is an overhaul of my GeneralConfirmation() method from the Finch Robot projects.
        /// 
        /// There are two variables in the method now.
        /// 1) userRes is used as the return value, and will not be reached if the user entered an incorrect key.
        /// 2) validKey ensures the user must enter the correct key. It is false, so that only keys ENTER and ESC set it to true to break the loop.
        /// 
        /// The do-while loop operates correctly here, unlike the old one.
        /// Before, the return statements were in the if statements, which ended the program there, even if the user input an incorrect statement.
        /// Now, a "false" value is not returned the method caller if the user pressed an invalid key.
        /// They will have to continue through the loop and try again.
        /// 
        /// The method now uses switch-case, as it is easier to type.
        /// </summary>
        static bool DualConfirmation()
        {
            Console.WriteLine("Press ENTER if so. Press ESC if not.");
            ConsoleKeyInfo keyEntry;
            bool userRes = false;
            bool validKey = false;
            do
            {
                keyEntry = Console.ReadKey(intercept: true);
                switch (keyEntry.Key)
                {
                    case ConsoleKey.Enter:
                        userRes = true;
                        validKey = true;
                        break;
                    case ConsoleKey.Escape:
                        userRes = false;
                        validKey = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERROR: incorrect '{0}' key entered.", keyEntry.Key);
                        Console.WriteLine("Retry your key submission.");
                        break;
                }
            } while (validKey != true);
            Console.WriteLine("---");
            return userRes;
        }
        #endregion
    }
}
