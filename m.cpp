#include <iostream>
#include <vector>
#include <string>
using namespace std;

class Student {
private:
    string name;
    vector<int> grades;

public:
    Student() {}

    Student(const string& name, const vector<int>& grades)
        : name(name), grades(grades) {}

    void print() const {
        cout << "Name: " << name << "\nGrades: ";
        for (int g : grades) cout << g << " ";
        cout << "\n";
    }

    string getName() const {
        return name;
    }
};

class Group {
private:
    string groupName;
    vector<Student> students;

public:
    Group() {}
    Group(const string& name) : groupName(name) {}

void addStudent(const Student& s) {
    students.push_back(s);
        cout << "Student added!\n";
    }

    void removeStudent(const string& name) {
        for (size_t i = 0; i < students.size(); i++) {
            if (students[i].getName() == name) {
                students.erase(students.begin() + i);
                cout << "Student removed!\n";
                return;
            }
        }
        cout << "Student not found!\n";
    }

    void printStudents() const {
        if (students.empty()) {
            cout << "No students in the group.\n";
            return;
        }

        cout << "\nGroup: " << groupName << "\n";
        cout << "=====================\n";

        for (const auto& s : students) {
            s.print();
            cout << "------------------\n";
        }
    }

    bool isCreated() const {
        return !groupName.empty();
    }
};

int main() {
    Group group;
    int choice;

    while (true) {
        cout << "\n=== MENU ===\n";
        cout << "1. Create group\n";
        cout << "2. Add student\n";
        cout << "3. Remove student\n";
        cout << "4. Show students\n";
        cout << "0. Exit\n";
        cout << "Choose: ";
        cin >> choice;

        if (choice == 0) break;

        switch (choice) {

        case 1: {
            string name;
            cout << "Enter group name: ";
            cin >> name;
            group = Group(name);
            cout << "Group created!\n";
            break;
        }

        case 2: {
            if (!group.isCreated()) {
                cout << "Create a group first!\n";
                break;
            }

            string name;
            cout << "Enter student name: ";
            cin >> name;

            int gradeCount;
            cout << "Number of grades: ";
            cin >> gradeCount;

            vector<int> grades(gradeCount);
            for (int i = 0; i < gradeCount; i++) {
                cout << "Grade " << i + 1 << ": ";
                cin >> grades[i];
            }

            group.addStudent(Student(name, grades));
            break;
        }

        case 3: {
            if (!group.isCreated()) {
                cout << "Create a group first!\n";
                break;
            }

            string name;
            cout << "Enter student name to remove: ";
            cin >> name;
            group.removeStudent(name);
            break;
        }

        case 4: {
            if (!group.isCreated()) {
                cout << "Create a group first!\n";
                break;
            }
            group.printStudents();
            break;
        }

        default:
            cout << "Invalid choice!\n";
        }
    }

    return 0;
}
