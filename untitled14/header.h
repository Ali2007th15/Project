#ifndef UNTITLED14_HEADER_H
#define UNTITLED14_HEADER_H
#include <iostream>
#include <string>
#include <vector>
#include <regex>
#include <map>
#include <fstream>

using namespace std;

class Person {
private:
    string name;
    string surname;

public:
    Person(const string& name, const string& surname) {
        this->name = name;
        this->surname = surname;
    }

    string Name() const {
        return name;
    }

    string Surname() const {
        return surname;
    }
};

class Cards {
private:
    Person* card_owner{};
    string cardNumber;
    string cvc;
    double balance{};
    string type;

public:
    Cards() = default;
    Cards(Person* card_owner, const string& cardNumber, const string& cvc, double balance, string type) {
        this->card_owner = card_owner;
        this->cardNumber = cardNumber;
        this->cvc = cvc;
        this->balance = balance;
        this->type = type;
    }

    Person* CardOwner() const {
        return card_owner;
    }

    string CardNumber() const {
        return cardNumber;
    }

    string CVC() const {
        return cvc;
    }

    double Balance() const {
        return balance;
    }

    string Type() const {
        return type;
    }

    bool Regex1() const {
        regex r_cardNumber("^[0-9]{16}");
        return regex_match(cardNumber, r_cardNumber);
    }

    bool Regex2() const {
        regex r_cvc("^[0-9]{3}");
        return regex_match(cvc, r_cvc);
    }
    void deposit(double amount) {
        balance += amount;
    }

    bool withdraw(double amount) {
        if (amount <= balance) {
            balance -= amount;
            return true;
        }
        return false;
    }
};

class Expense {
public:
    string category;
    double amount;
    string date;

    Expense(string category, double amount, string date) : category(category), amount(amount), date(date) {}
};
class Wallet {
private:
    Person *wallet_owner;
    static const int maxCards = 1000;
    Cards *cards[maxCards];
    int numCards;
    vector<Expense> expenses;

public:
    Wallet(Person *wallet_owner) : wallet_owner(wallet_owner), numCards(0) {
        for (int i = 0; i < maxCards; ++i) {
            cards[i] = nullptr;
        }
    }

    Person *getWalletOwner() const {
        return wallet_owner;
    }

    void addCard(Cards *card) {
        if (numCards < maxCards) {
            cards[numCards++] = card;
        } else {
            cout << "Wallet is full. Cannot add more cards." << endl;
        }
    }

    void deposit(double amount) {
        if (numCards == 0) {
            cout << "No cards found in the wallet." << endl;
            return;
        }
        double amountCard = amount / numCards;
        for (int i = 0; i < numCards; ++i) {
            cards[i]->deposit(amountCard);
        }
    }

    double TotalBalance() const {
        double totalBalance{};
        for (int i = 0; i < numCards; ++i) {
            totalBalance += cards[i]->Balance();
        }
        return totalBalance;
    }

    void addExpense(string category, double amount, string date) {
        expenses.push_back(Expense(category, amount, date));
    }

    void ReportByDay(string date) {
        double totalExpenses = 0;

        for (const Expense &expense: expenses) {
            if (expense.date == date) {
                cout << "Category: " << expense.category << ", Amount: " << expense.amount << endl;
                totalExpenses += expense.amount;
            }
        }

        cout << "Total Expenses: " << totalExpenses << endl;
    }

    void ReportByWeek(string startDate, string endDate) {
        double totalExpenses = 0;

        for (const Expense &expense: expenses) {
            if (expense.date >= startDate && expense.date <= endDate) {
                cout << "Category: " << expense.category << ", Amount: " << expense.amount << endl;
                totalExpenses += expense.amount;
            }
        }

        cout << "Total Expenses: " << totalExpenses << endl;
    }

    void ReportByMonth(string month, string year) {
        double totalExpenses = 0;

        for (const Expense &expense: expenses) {
            if (expense.date.substr(3, 7) == month + "-" + year) {
                cout << "Category: " << expense.category << ", Amount: " << expense.amount << endl;
                totalExpenses += expense.amount;
            }
        }


    }

    void TopExpensesByWeek(string startDate, string endDate) {
        map<string, double> categoryExpenses;

        for (const Expense &expense: expenses) {
            if (expense.date >= startDate && expense.date <= endDate) {
                categoryExpenses[expense.category] += expense.amount;
            }
        }
        cout << "Top 3 Expenses by Week:" << endl;
        int count = 0;
        for (const auto &pair: categoryExpenses) {
            if (count >= 3) {
                break;
            }
            cout << "Category: " << pair.first << ", Amount: " << pair.second << endl;
            count++;
        }
    }

    void TopExpensesByMonth(string month, string year) {
        map<string, double> categoryExpenses;

        for (const Expense &expense: expenses) {
            if (expense.date.substr(3, 7) == month + "-" + year) {
                cout << "Expense Date: " << expense.date << endl;  // Debugging output
                categoryExpenses[expense.category] += expense.amount;
            }
        }

        cout << "Top 3 Expenses by Month:" << endl;
        int count = 0;
        for (const auto &pair: categoryExpenses) {
            if (count >= 3) {
                break;
            }
            cout << "Category: " << pair.first << ", Amount: " << pair.second << endl;
            count++;
        }
    }

    void save(string filename) {
        ofstream file(filename);

        if (!file.is_open()) {
            cout << "Failed to open the file." << endl;
            return;
        }
        file << "Wallet Owner: " << wallet_owner->Name() << " " << wallet_owner->Surname() << endl;
        file << "Cards:" << endl;
        for (int i = 0; i < numCards; ++i) {
            Cards *card = cards[i];
            file << "Card Owner: " << card->CardOwner()->Name() << " " << card->CardOwner()->Surname() << endl;
            file << "Card Number: " << card->CardNumber() << endl;
            file << "CVC: " << card->CVC() << endl;
            file << "Balance: " << card->Balance() << " AZN" << endl;
            file << "Type: " << card->Type() << endl;
        }
        file << "Expenses:" << endl;
        for (const Expense &expense: expenses) {
            file << "Category: " << expense.category << ", Amount: " << expense.amount << ", Date: "
                 << expense.date << endl;
        }
        file << endl << "Total balance: " << TotalBalance() << " AZN" << endl;
        file.close();
        cout << "All information saved to file: " << filename << endl;
    };
};

#endif
