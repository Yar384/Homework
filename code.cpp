#include <iostream>
using namespace std;

class DoublyLinkedList {
private:
    struct Node {
        int value;
        Node* prev;
        Node* next;

        Node(int val) : value(val), prev(nullptr), next(nullptr) {}
    };

    Node* head;
    Node* tail;
    int count;

public:
    DoublyLinkedList() : head(nullptr), tail(nullptr), count(0) {}

    ~DoublyLinkedList() {
        clear();
    }

    // 1
    void push_front(int value) {
        Node* newNode = new Node(value);
        newNode->next = head;
        if (head) head->prev = newNode;
        else tail = newNode; // Якщо список був порожній
        head = newNode;
        count++;
    }

    // 2
    void push_back(int value) {
        Node* newNode = new Node(value);
        newNode->prev = tail;
        if (tail) tail->next = newNode;
        else head = newNode; // Якщо список був порожній
        tail = newNode;
        count++;
    }

    // 3
    void pop_front() {
        if (!head) return;
        Node* temp = head;
        head = head->next;
        if (head) head->prev = nullptr;
        else tail = nullptr;
        delete temp;
        count--;
    }

    // 4
    void pop_back() {
        if (!tail) return;
        Node* temp = tail;
        tail = tail->prev;
        if (tail) tail->next = nullptr;
        else head = nullptr;
        delete temp;
        count--;
    }

    // 5
    void insert(int position, int value) {
        if (position <= 0) {
            push_front(value);
            return;
        }
        if (position >= count) {
            push_back(value);
            return;
        }

        Node* current = head;
        for (int i = 0; i < position; ++i)
            current = current->next;

        Node* newNode = new Node(value);
        newNode->prev = current->prev;
        newNode->next = current;
        current->prev->next = newNode;
        current->prev = newNode;
        count++;
    }

    // 6
    void erase(int position) {
        if (position < 0 || position >= count) return;

        Node* current = head;
        for (int i = 0; i < position; ++i)
            current = current->next;

        if (current->prev) current->prev->next = current->next;
        else head = current->next;

        if (current->next) current->next->prev = current->prev;
        else tail = current->prev;

        delete current;
        count--;
    }

    // 7
    int find(int value) {
        Node* current = head;
        int index = 0;
        while (current) {
            if (current->value == value)
                return index;
            current = current->next;
            index++;
        }
        return -1; // Якщо не знайдено
    }

    // 8
    void clear() {
        while (head) {
            pop_front();
        }
    }

    // 9
    int size() const {
        return count;
    }

    // 10
    bool empty() const {
        return count == 0;
    }

    // 11
    void print_forward() const {
        Node* current = head;
        while (current) {
            cout << current->value << " ";
            current = current->next;
        }
        cout << endl;
    }

    // 12
    void print_backward() const {
        Node* current = tail;
        while (current) {
            cout << current->value << " ";
            current = current->prev;
        }
        cout << endl;
    }
};

int main() {
    DoublyLinkedList list;

    list.push_back(10);
    list.push_back(20);
    list.push_back(30);
    list.push_front(5);
    list.insert(2, 15);

    cout << "Вперед: ";
    list.print_forward();

    cout << "Назад: ";
    list.print_backward();

    cout << "Знайти 20: позиція - " << list.find(20) << endl;

    list.erase(2);
    cout << "Після видалення позиції 2: ";
    list.print_forward();

    cout << "Розмір списку: " << list.size() << endl;
    cout << "Порожній?: " << (list.empty() ? "Так" : "Ні") << endl;

    list.clear();
    cout << "Після очищення — порожній?: " << (list.empty() ? "Так" : "Ні") << endl;

    return 0;
}
