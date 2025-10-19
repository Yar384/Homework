#include <iostream>
#include <cstring>
using namespace std;

class String {
private:
    char* str;

public:
    String(const char* s = "") {
        str = new char[strlen(s) + 1];
        strcpy(str, s);
    }

    ~String() {
        delete[] str;
    }

    const char* get() const {
        return str;
    }

    void print() const {
        cout << str;
    }
};

//Наслідування
class SmartString : public String {
private:
    int length;

public:
    SmartString(const char* s = "") : String(s) {
        length = strlen(s);
    }

    void info() const {
        cout << "String length: " << length << endl;
    }
};

//Композиція
class Document {
private:
    String content;

public:
    Document(const char* text) : content(text) {}

    void show() const {
        cout << "Document content: ";
        content.print();
        cout << endl;
    }
};

//Aагрегація
class Notebook {
private:
    Document* doc;

public:
    Notebook(Document* d) : doc(d) {}

    void open() const {
        if (doc)
            doc->show();
        else
            cout << "No document attached." << endl;
    }
};

int main() {
    SmartString s("Hello World");
    s.info();

    Document d("This is a composition example.");
    d.show();

    Notebook n(&d);
    n.open();

    return 0;
}
