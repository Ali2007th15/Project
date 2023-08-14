#include "header.h"

int main() {
    Person person1("Ali", "Ismayil");
    Person person2("Teymur", "Aliyev");

    Wallet wallet1(&person1);
    Wallet wallet2(&person2);

    Cards card1(&person1, "6754234562157096", "753", 5000, "debit");
    Cards card2(&person2, "8765408473858473", "456", 6000, "credit");

    if (card1.Regex1() && card1.Regex2()) {
        wallet1.addCard(&card1);
    } else {
        cout << "Invalid card number or CVC for card1" << endl;
    }

    if (card2.Regex1() && card2.Regex2()) {
        wallet1.addCard(&card2);
    } else {
        cout << "Invalid card number or CVC for card2" << endl;
    }

    card1.deposit(1000);
    card2.withdraw(2000);

    wallet1.addExpense("Food", 50, "2023-01-01");
    wallet1.addExpense("Transportation", 20, "2023-01-01");
    wallet1.addExpense("Shopping", 100, "2023-01-02");
    wallet1.addExpense("Food", 30, "2023-01-03");

    wallet1.ReportByDay("2023-01-01");
    wallet1.ReportByWeek("2023-01-01", "2023-01-07");
    wallet1.ReportByMonth("01", "2023");

    wallet1.TopExpensesByWeek("2023-01-01", "2023-01-07");
    wallet1.TopExpensesByMonth("01", "2023");
    wallet1.save("Wallet.txt");

    return 0;
}
