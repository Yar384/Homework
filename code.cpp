#include <iostream>
#include <string>
using namespace std;

class Audio {
public:
    virtual void Play() = 0; // чисто віртуальний метод
    virtual ~Audio() {}
};

class Song : public Audio {
private:
    string title;
    string artist;

public:
    Song(const string& t, const string& a) : title(t), artist(a) {}

    void Play() override {
        cout << "Playing song: " << title << " by " << artist << "\n";
    }
};

class Podcast : public Audio {
private:
    string host;
    string topic;

public:
    Podcast(const string& h, const string& t) : host(h), topic(t) {}

    void Play() override {
        cout << "Podcast on " << topic << " hosted by " << host << "\n";
    }
};

class Audiobook : public Audio {
private:
    string bookTitle;
    string author;
    string voice;

public:
    Audiobook(const string& bt, const string& a, const string& v)
        : bookTitle(bt), author(a), voice(v) {}

    void Play() override {
        cout << "Listening to audiobook: " << bookTitle
             << " by " << author
             << ". Read by " << voice << ".\n";
    }
};

int main() {
    int choice;
    Audio* audio = nullptr;

    cout << "Select audio type:\n";
    cout << "1 - Song\n";
    cout << "2 - Podcast\n";
    cout << "3 - Audiobook\n";
    cout << "Your choice: ";
    cin >> choice;

    if (choice == 1) {
        string title, artist;

        cout << "Enter song title: ";
        getline(cin, title);
        cout << "Enter artist: ";
        getline(cin, artist);

        audio = new Song(title, artist);
    }
    else if (choice == 2) {
        string host, topic;

        cout << "Enter podcast host: ";
        getline(cin, host);
        cout << "Enter podcast topic: ";
        getline(cin, topic);

        audio = new Podcast(host, topic);
    }
    else if (choice == 3) {
        string bookTitle, author, voice;

        cout << "Enter audiobook title: ";
        getline(cin, bookTitle);
        cout << "Enter author: ";
        getline(cin, author);
        cout << "Enter voice actor: ";
        getline(cin, voice);

        audio = new Audiobook(bookTitle, author, voice);
    }
    else {
        cout << "Invalid choice!\n";
        return 0;
    }

    cout << "\n--- Playing ---\n";
    audio->Play();

    delete audio;
    return 0;
}
