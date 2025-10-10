#include <iostream>
#include <cstring>
using namespace std;

class String {
private:
    char* str;
    int length;

public:
    String(const char* s = "") {
        length = strlen(s);
        str = new char[length + 1];
        strcpy(str, s);
    }

    String(const String& other) {
        length = other.length;
        str = new char[length + 1];
        strcpy(str, other.str);
    }

    String(String&& other) noexcept {
        str = other.str;
        length = other.length;
        other.str = nullptr;
        other.length = 0;
    }

    String& operator=(const String& other) {
        if (this != &other) {
            delete[] str;
            length = other.length;
            str = new char[length + 1];
            strcpy(str, other.str);
        }
        return *this;
    }

    String& operator=(String&& other) noexcept {
        if (this != &other) {
            delete[] str;
            str = other.str;
            length = other.length;
            other.str = nullptr;
            other.length = 0;
        }
        return *this;
    }

    ~String() {
        delete[] str;
    }

    int getLength() {
        return length;
    }

    char* getStr() {
        return str;
    }

    const char* c_str() {
        return str;
    }

    char& operator[](int index) {
        return str[index];
    }

    const char& operator[](int index) const {
        return str[index];
    }

    friend String operator+(const String& a, const String& b);

    bool empty() {
        return length == 0;
    }

    String substr(int start, int end) {
        if (start < 0) start = 0;
        if (end > length) end = length;
        if (start >= end) return String("");

        int newLen = end - start;
        char* buffer = new char[newLen + 1];
        for (int i = 0; i < newLen; i++) {
            buffer[i] = str[start + i];
        }
        buffer[newLen] = '\0';
        String temp(buffer);
        delete[] buffer;
        return temp;
    }

    void replace(int start, int count, const char* newStr) {
        if (start < 0 || start >= length) return;

        int newStrLen = strlen(newStr);
        if (start + count > length) count = length - start;

        int newLength = length - count + newStrLen;
        char* buffer = new char[newLength + 1];

        strncpy(buffer, str, start);
        buffer[start] = '\0';
        strcat(buffer, newStr);
        strcat(buffer, str + start + count);

        delete[] str;
        str = buffer;
        length = newLength;
    }

    void insert(int index, const char* newStr) {
        if (index < 0) index = 0;
        if (index > length) index = length;

        int newStrLen = strlen(newStr);
        int newLength = length + newStrLen;
        char* buffer = new char[newLength + 1];

        strncpy(buffer, str, index);
        buffer[index] = '\0';

        strcat(buffer, newStr);
        strcat(buffer, str + index);

        delete[] str;
        str = buffer;
        length = newLength;
    }
};

String operator+(const String& a, const String& b) {
    int newLength = a.length + b.length;
    char* buffer = new char[newLength + 1];

    strcpy(buffer, a.str);
    strcat(buffer, b.str);

    String temp(buffer);
    delete[] buffer;
    return temp;
}

int main() {
    String s1("Hello");
    String s2(" World");

    cout << "s1: " << s1.getStr() << endl;
    cout << "s2: " << s2.c_str() << endl;

    String s3 = s1 + s2;
    cout << "s1 + s2 = " << s3.getStr() << endl;

    //empty()
    cout << "s1 empty? " << (s1.empty() ? "True" : "False") << endl;

    //substr()
    String part = s3.substr(0, 5);
    cout << "substr(0, 5): " << part.getStr() << endl;

    //replace()
    s3.replace(6, 5, "Universe");
    cout << "After replace: " << s3.getStr() << endl;

    //insert()
    s3.insert(5, ",");
    cout << "After insert: " << s3.getStr() << endl;

    return 0;
}
