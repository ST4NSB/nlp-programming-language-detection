# Natural Language Processing programming
## Details
A library to program, train & process words to detect the high level programming language from a (txt) file.  
Currently supports:  
+ **C/C++** (training data in: [bin\\Debug\\netcoreapp2.1\\files\\**training_cpp.txt**])  
+ **Java** (training data in: [bin\\Debug\\netcoreapp2.1\\files\\**training_java.txt**])

## Install
+ Download: **git clone https://github.com/ST4NSB/nlp-programming-language-detection.git**
+ Open project file  
+ Add Reference to the project -> [NLP\\..\bin\Debug\\**NLP.dll**] (might need to remove it first from the project solution)  
+ Compile & Run  

## Example
In [bin\\Debug\\netcoreapp2.1\\files\\**test_file.txt**] there is a sample program (written in C++) for testing

```cpp
/* This is a test file  */
/* Write code in here and let the model predict it */

// Binary search algorithm
#include <algorithm>
#include <vector>
#include <iostream>
using namespace std;
int bsearch(const vector<int> &arr, int l, int r, int q)
{ 
    while (l <= r) 
    {
        int mid = l + (r-l)/2;
        if (arr[mid] == q) return mid; 
        
        if (q < arr[mid]) { r = mid — 1; } 
        else              { l = mid + 1; }
    }
    return -1; //not found
}

int main()
{
    int query = 10; 
    int arr[] = {2, 4, 6, 8, 10, 12};
    int N = sizeof(arr)/sizeof(arr[0]);
    vector<int> v(arr, arr + N); 
    
    //sort input array
    sort(v.begin(), v.end());
    int idx;
    idx = bsearch(v, 0, v.size(), query);
    if (idx != -1)
        cout << "custom binary_search: found at index " << idx;    
    else 
        cout << "custom binary_search: not found";
    return 0;
}
```

The output file will print: 
>The model predicts: C++ as the language of the file(files/test_file.txt).  
>Prediction procent: 0.997382117908361

## License
This project is licensed unde **MIT** [https://opensource.org/licenses/MIT/]

