#include <iostream>
#include <fstream>
#include <random>
using namespace std;

class GuessGame {
private:
    int secret;
    int attempts;
    ofstream log;

public:
    GuessGame(const string& filename) : attempts(0) {
        // Генератор справжніх випадкових чисел
        random_device rd;
        mt19937 gen(rd());
        uniform_int_distribution<int> dist(1, 500);

        secret = dist(gen);

        log.open(filename, ios::app);
        if (!log.is_open()) {
            cerr << "Помилка відкриття файлу!\n";
        }
        log << "Загадане число: " << secret << "\n";
    }

    ~GuessGame() {
        if (log.is_open()) {
            log << "-------------------------\n";
            log.close();
        }
    }

    void play() {
        int guess;
        cout << "Вгадайте число від 1 до 500.\n";

        while (true) {
            cout << "Ваш варіант: ";
            cin >> guess;

            attempts++;
            log << "Спроба " << attempts << ": " << guess;

            if (guess > secret) {
                cout << "Загадане число менше.\n";
                log << " (більше)\n";
            }
            else if (guess < secret) {
                cout << "Загадане число більше.\n";
                log << " (менше)\n";
            }
            else {
                cout << "Вітаю! Ви вгадали число за " << attempts << " спроб!\n";
                log << " (вгадано)\n";
                break;
            }
        }
    }
};

int main() {
    GuessGame game("game_log.txt");
    game.play();
    return 0;
}
