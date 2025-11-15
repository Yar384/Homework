#include <iostream>
#include <string>
using namespace std;

//1
void printType(void* ptr, const string& typeName) {
    cout << "Type: " << typeName << endl;
}

//2
void printChar(int value) {
    char* ch = reinterpret_cast<char*>(&value);
    cout << "Character: " << *ch << endl;
}

int main() {
    //1
    int a = 10;
    double b = 3.14;
    char c = 'A';

    printType(&a, "int");
    printType(&b, "double");
    printType(&c, "char");

    //2
    int x;
    cout << "Enter number: ";
    cin >> x;

    printChar(x);

    return 0;
}
