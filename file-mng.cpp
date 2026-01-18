#include <iostream>
#include <filesystem>
#include <fstream>
#include <string>
using namespace std;
namespace fs = filesystem;

class FileManager {
public:
    virtual void showDisks() = 0;
    virtual void createFile(string path) = 0;
    virtual void createFolder(string path) = 0;
    virtual void removeItem(string path) = 0;
    virtual void renameItem(string oldName, string newName) = 0;
    virtual void copyItem(string from, string to) = 0;
    virtual void moveItem(string from, string to) = 0;
    virtual unsigned long long getSize(string path) = 0;
    virtual void search(string path, string mask) = 0;
};

class SimpleFileManager : public FileManager {
public:
    void showDisks() override {
#ifdef _WIN32
        for (char d = 'A'; d <= 'Z'; d++) {
            string drive = string(1, d) + ":\\";
            if (fs::exists(drive)) {
                cout << drive << endl;
            }
        }
#else
        cout << "/" << endl;
#endif
    }

    void createFile(string path) override {
        ofstream file(path);
        file.close();
    }

    void createFolder(string path) override {
        fs::create_directory(path);
    }

    void removeItem(string path) override {
        fs::remove_all(path);
    }

    void renameItem(string oldName, string newName) override {
        fs::rename(oldName, newName);
    }

    void copyItem(string from, string to) override {
        fs::copy(from, to, fs::copy_options::recursive);
    }

    void moveItem(string from, string to) override {
        fs::rename(from, to);
    }

    unsigned long long getSize(string path) override {
        if (fs::is_regular_file(path)) {
            return fs::file_size(path);
        }

        unsigned long long totalSize = 0;
        for (auto& p : fs::recursive_directory_iterator(path)) {
            if (fs::is_regular_file(p)) {
                totalSize += fs::file_size(p);
            }
        }
        return totalSize;
    }

    void search(string path, string mask) override {
        for (auto& p : fs::recursive_directory_iterator(path)) {
            if (p.path().filename().string().find(mask) != string::npos) {
                cout << p.path() << endl;
            }
        }
    }
};

int main() {
    SimpleFileManager manager;
    int choice;
    string a, b;

    while (true) {
        cout << "1. Show disks\n";
        cout << "2. Create file\n";
        cout << "3. Create folder\n";
        cout << "4. Delete file/folder\n";
        cout << "5. Rename file/folder\n";
        cout << "6. Copy file/folder\n";
        cout << "7. Move file/folder\n";
        cout << "8. Get size\n";
        cout << "9. Search by mask\n";
        cout << "0. Exit\n";
        cout << "Choose: ";

        cin >> choice;

        if (choice == 0)
            break;

        switch (choice) {
        case 1:
            manager.showDisks();
            break;
        case 2:
            cout << "Enter file path: ";
            cin >> a;
            manager.createFile(a);
            break;
        case 3:
            cout << "Enter folder path: ";
            cin >> a;
            manager.createFolder(a);
            break;
        case 4:
            cout << "Enter path: ";
            cin >> a;
            manager.removeItem(a);
            break;
        case 5:
            cout << "Old name: ";
            cin >> a;
            cout << "New name: ";
            cin >> b;
            manager.renameItem(a, b);
            break;
        case 6:
            cout << "From: ";
            cin >> a;
            cout << "To: ";
            cin >> b;
            manager.copyItem(a, b);
            break;
        case 7:
            cout << "From: ";
            cin >> a;
            cout << "To: ";
            cin >> b;
            manager.moveItem(a, b);
            break;
        case 8:
            cout << "Enter path: ";
            cin >> a;
            cout << "Size: " << manager.getSize(a) << " bytes\n";
            break;
        case 9:
            cout << "Search path: ";
            cin >> a;
            cout << "Mask: ";
            cin >> b;
            manager.search(a, b);
            break;
        default:
            cout << "Invalid choice!\n";
        }
    }

    return 0;
}
