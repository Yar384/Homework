#include <iostream>
using namespace std;

//Вузол
struct Node {
    int data;
    Node* next;

    Node(int d) : data(d), next(nullptr) {}
};

//Stack
class Stack {
private:
    Node* top; //вершина

public:
    Stack() : top(nullptr) {}

    ~Stack() {
        while (!isEmpty()) pop();
    }

    //(push)
    void push(int value) {
        Node* newNode = new Node(value);
        newNode->next = top;
        top = newNode;
    }

    //(pop)
    void pop() {
        if (isEmpty()) {
            cout << "Stack is empty!\n";
            return;
        }
        Node* temp = top;
        top = top->next;
        delete temp;
    }

    //(peek)
    int peek() {
        if (isEmpty()) {
            cout << "Stack is empty!\n";
            return -1;
        }
        return top->data;
    }

    bool isEmpty() {
        return top == nullptr;
    }

    void print() {
        Node* curr = top;
        cout << "Stack: ";
        while (curr) {
            cout << curr->data << " ";
            curr = curr->next;
        }
        cout << endl;
    }
};

//Queue
class Queue {
private:
    Node* front; //початок
    Node* rear; //кінець

public:
    Queue() : front(nullptr), rear(nullptr) {}

    ~Queue() {
        while (!isEmpty()) dequeue();
    }

    void enqueue(int value) {
        Node* newNode = new Node(value);
        if (rear == nullptr) {
            front = rear = newNode;
        } else {
            rear->next = newNode;
            rear = newNode;
        }
    }

    void dequeue() {
        if (isEmpty()) {
            cout << "Queue is empty!\n";
            return;
        }
        Node* temp = front;
        front = front->next;
        if (front == nullptr)
            rear = nullptr;
        delete temp;
    }

    int peek() {
        if (isEmpty()) {
            cout << "Queue is empty!\n";
            return -1;
        }
        return front->data;
    }

    bool isEmpty() {
        return front == nullptr;
    }

    void print() {
        Node* curr = front;
        cout << "Queue: ";
        while (curr) {
            cout << curr->data << " ";
            curr = curr->next;
        }
        cout << endl;
    }
};

int main() {
    Stack st;
    st.push(10);
    st.push(20);
    st.push(30);
    st.print();
    cout << "Top element: " << st.peek() << endl;
    st.pop();
    st.print();

    Queue q;
    q.enqueue(1);
    q.enqueue(2);
    q.enqueue(3);
    q.print();
    cout << "Front element: " << q.peek() << endl;
    q.dequeue();
    q.print();

    return 0;
}
