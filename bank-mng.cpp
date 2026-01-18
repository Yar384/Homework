#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <ctime>
using namespace std;

class Account {
protected:
    string name;
    double balance;

public:
    Account(string n) : name(n), balance(0) {}

    virtual string getType() = 0;

    void addMoney(double amount) {
        balance += amount;
    }

    bool spendMoney(double amount) {
        if (balance >= amount) {
            balance -= amount;
            return true;
        }
        return false;
    }

    double getBalance() {
        return balance;
    }

    string getName() {
        return name;
    }
};

class Wallet : public Account {
public:
    Wallet(string n) : Account(n) {}

    string getType() override {
        return "Wallet";
    }
};

class DebitCard : public Account {
public:
    DebitCard(string n) : Account(n) {}

    string getType() override {
        return "Debit Card";
    }
};

class CreditCard : public Account {
public:
    CreditCard(string n) : Account(n) {}

    string getType() override {
        return "Credit Card";
    }
};

class Expense {
public:
    string category;
    double amount;
    time_t date;

    Expense(string c, double a) {
        category = c;
        amount = a;
        date = time(0);
    }
};

class FinanceManager {
private:
    vector<Account*> accounts;
    vector<Expense> expenses;

public:
    void addAccount(Account* acc) {
        accounts.push_back(acc);
    }

    void showAccounts() {
        for (size_t i = 0; i < accounts.size(); i++) {
            cout << i << ". " << accounts[i]->getType()
                 << " (" << accounts[i]->getName()
                 << ") Balance: " << accounts[i]->getBalance() << endl;
        }
    }

    void addMoney(int index, double amount) {
        accounts[index]->addMoney(amount);
    }

    void addExpense(int index, double amount, string category) {
        if (accounts[index]->spendMoney(amount)) {
            expenses.push_back(Expense(category, amount));
        } else {
            cout << "Not enough money!\n";
        }
    }

    bool inPeriod(time_t expenseDate, int days) {
        time_t now = time(0);
        double diff = difftime(now, expenseDate);
        return diff <= days * 86400;
    }

    void reportByPeriod(int days, string filename) {
        ofstream file(filename);
        double total = 0;

        for (auto& e : expenses) {
            if (inPeriod(e.date, days)) {
                file << e.category << " : " << e.amount << endl;
                total += e.amount;
            }
        }

        file << "Total: " << total << endl;
        file.close();
    }

    void topExpenses(int days, string filename) {
        vector<double> top(3, 0);

        for (auto& e : expenses) {
            if (inPeriod(e.date, days)) {
                for (int i = 0; i < 3; i++) {
                    if (e.amount > top[i]) {
                        top.insert(top.begin() + i, e.amount);
                        top.pop_back();
                        break;
                    }
                }
            }
        }

        ofstream file(filename);
        for (int i = 0; i < 3; i++) {
            file << "TOP " << i + 1 << ": " << top[i] << endl;
        }
        file.close();
    }

    void topCategories(int days, string filename) {
        vector<string> categories;
        vector<double> sums;

        for (auto& e : expenses) {
            if (inPeriod(e.date, days)) {
                bool found = false;
                for (size_t i = 0; i < categories.size(); i++) {
                    if (categories[i] == e.category) {
                        sums[i] += e.amount;
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    categories.push_back(e.category);
                    sums.push_back(e.amount);
                }
            }
        }

        ofstream file(filename);
        for (size_t i = 0; i < categories.size(); i++) {
            file << categories[i] << " : " << sums[i] << endl;
        }
        file.close();
    }
};

int main() {
    FinanceManager manager;
    int choice, type, index;
    string name, category;
    double amount;

    while (true) {
        cout << "1. Add account\n";
        cout << "2. Show accounts\n";
        cout << "3. Add money\n";
        cout << "4. Add expense\n";
        cout << "5. Report (day)\n";
        cout << "6. Report (week)\n";
        cout << "7. Report (month)\n";
        cout << "8. TOP-3 expenses (week)\n";
        cout << "9. TOP-3 expenses (month)\n";
        cout << "0. Exit\n";
        cout << "Choose: ";
        cin >> choice;

        if (choice == 0)
            break;

        switch (choice) {
        case 1:
            cout << "1.Wallet 2.Debit Card 3.Credit Card: ";
            cin >> type;
            cout << "Name: ";
            cin >> name;

            if (type == 1) manager.addAccount(new Wallet(name));
            if (type == 2) manager.addAccount(new DebitCard(name));
            if (type == 3) manager.addAccount(new CreditCard(name));
            break;

        case 2:
            manager.showAccounts();
            break;

        case 3:
            manager.showAccounts();
            cout << "Account index: ";
            cin >> index;
            cout << "Amount: ";
            cin >> amount;
            manager.addMoney(index, amount);
            break;

        case 4:
            manager.showAccounts();
            cout << "Account index: ";
            cin >> index;
            cout << "Amount: ";
            cin >> amount;
            cout << "Category: ";
            cin >> category;
            manager.addExpense(index, amount, category);
            break;

        case 5:
            manager.reportByPeriod(1, "report_day.txt");
            break;
        case 6:
            manager.reportByPeriod(7, "report_week.txt");
            break;
        case 7:
            manager.reportByPeriod(30, "report_month.txt");
            break;
        case 8:
            manager.topExpenses(7, "top_expenses_week.txt");
            break;
        case 9:
            manager.topExpenses(30, "top_expenses_month.txt");
            break;
        }
    }

    return 0;
}
