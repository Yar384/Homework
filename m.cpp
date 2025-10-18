#include <iostream>
#include <string>
using namespace std;

class Pet {
protected:
    string name;
    string species;
    string sound;

public:
    Pet(const string& n, const string& s, const string& snd)
        : name(n), species(s), sound(snd) {
    }

    virtual void info() const {
        cout << "Name: " << name << endl;
        cout << "Species: " << species << endl;
        cout << "Sound: " << sound << endl;
    }

    virtual void makeSound() const {
        cout << name << "Says: " << sound<< endl;
    }

    virtual ~Pet() {}
};

class Dog : public Pet {
private:
    string breed;

public:
    Dog(const string& name, const string& breed)
        : Pet(name, "Dog", "Woof"), breed(breed) {
    }

    void info() const override {
        cout << "Dog: " << name << endl;
        cout << "Breed: " << breed << endl;
        cout << "Sound: " << sound << endl;
    }

    void makeSound() const override {
        cout << name << "Says: " << sound << endl;
    }
};

class Cat : public Pet {
private:
    string color;

public:
    Cat(const string& name, const string& color)
        : Pet(name, "Cat", "Meow"), color(color) {
    }

    void info() const override {
        cout << "Cat: " << name << endl;
        cout << "Color: " << color << endl;
        cout << "Sound: " << sound << endl;
    }

    void makeSound() const override {
        cout << name << "Says: " << sound << endl;
    }
};

class Parrot : public Pet {
private:
    string favoriteWord;

public:
    Parrot(const string& name, const string& favoriteWord)
        : Pet(name, "Parrot", "Squawk"), favoriteWord(favoriteWord) {
    }

    void info() const override {
        cout << "Parrot: " << name << endl;
        cout << "Favorite word: " << favoriteWord << endl;
        cout << "Sound: " << sound << endl;
    }

    void makeSound() const override {
        cout << name << "Says: \"" << favoriteWord << "!\" And then squawks " << sound << endl;
    }
};

int main() {
    Dog dog("Buddy", "Labrador");
    Cat cat("Misty", "Gray");
    Parrot parrot("Kiwi", "Hello");

    Pet* pets[] = { &dog, &cat, &parrot };

    for (Pet* p : pets) {
        p->info();
        p->makeSound();
        cout << "---------------------------" << endl;
    }

    return 0;
}
