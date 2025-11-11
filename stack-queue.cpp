#include <iostream>
#include <fstream>
using namespace std;

class Shape {
public:
    virtual void Show() = 0;
    virtual void Save(ofstream& file) = 0;
    virtual void Load(ifstream& file) = 0;
    virtual ~Shape() {}
};

class Square : public Shape {
private:
    double side;
public:
    Square(double s = 0) : side(s) {}

    void Show() override {
        cout << "Square, side = " << side << endl;
    }

    void Save(ofstream& file) override {
        file << "Square " << side << endl;
    }

    void Load(ifstream& file) override {
        file >> side;
    }
};

class Rectangle : public Shape {
private:
    double width, height;
public:
    Rectangle(double w = 0, double h = 0) : width(w), height(h) {}

    void Show() override {
        cout << "Rectangle, width = " << width << ", height = " << height << endl;
    }

    void Save(ofstream& file) override {
        file << "Rectangle " << width << " " << height << endl;
    }

    void Load(ifstream& file) override {
        file >> width >> height;
    }
};

class Circle : public Shape {
private:
    double radius;
public:
    Circle(double r = 0) : radius(r) {}

    void Show() override {
        cout << "Circle, radius = " << radius << endl;
    }

    void Save(ofstream& file) override {
        file << "Circle " << radius << endl;
    }

    void Load(ifstream& file) override {
        file >> radius;
    }
};

int main() {
    Shape* shapes[3];
    shapes[0] = new Square(5);
    shapes[1] = new Rectangle(3, 6);
    shapes[2] = new Circle(4);

    ofstream out("shapes.txt");
    for (int i = 0; i < 3; i++) {
        shapes[i]->Save(out);
    }
    out.close();

    for (int i = 0; i < 3; i++) {
        delete shapes[i];
    }

    Shape* loaded[3];
    ifstream in("shapes.txt");
    for (int i = 0; i < 3; i++) {
        string type;
        in >> type;
        if (type == "Square") {
            loaded[i] = new Square();
        }
        else if (type == "Rectangle") {
            loaded[i] = new Rectangle();
        }
        else if (type == "Circle") {
            loaded[i] = new Circle();
        }
        loaded[i]->Load(in);
    }
    in.close();

    cout << "Loaded shapes:" << endl;
    for (int i = 0; i < 3; i++) {
        loaded[i]->Show();
        delete loaded[i];
    }

    return 0;
}
