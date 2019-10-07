# NLP Programming Language Detection
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
> The model predicts: C++ as the language of the file(files/test_file.txt).    
> Prediction procent: 0.997382117908361    

Confidence procentage: **99.73%**   

---
An example for Java below (binary search algorithm)

```java
/* This is a test file  */
/* Write code in here and let the model predict it */

package Searches;

import java.util.Arrays;
import java.util.Random;
import java.util.concurrent.ThreadLocalRandom;
import java.util.stream.IntStream;

import static java.lang.String.format;

class BinarySearch implements SearchAlgorithm {
    @Override
    public  <T extends Comparable<T>> int find(T[] array, T key) {
        return search(array, key, 0, array.length);
    }

    private <T extends Comparable<T>> int search(T array[], T key, int left, int right){
        if (right < left) return -1; // this means that the key not found

        // find median
        int median = (left + right) >>> 1;
        int comp = key.compareTo(array[median]);

        if (comp == 0) {
            return median;
        } else if (comp < 0) {
            return search(array, key, left, median - 1);
        } else {
            return search(array, key, median + 1, right);
        }
    }

    // Driver Program
    public static void main(String[] args) {
        // Just generate data
        Random r = ThreadLocalRandom.current();

        int size = 100;
        int maxElement = 100000;

        Integer[] integers = IntStream.generate(() -> 
            r.nextInt(maxElement)).limit(size).sorted().boxed().toArray(Integer[]::new);


        // The element that should be found
        int shouldBeFound = integers[r.nextInt(size - 1)];

        BinarySearch search = new BinarySearch();
        int atIndex = search.find(integers, shouldBeFound);

        System.out.println(format(
            "Should be found: %d. Found %d at index %d. An array length %d",
            shouldBeFound, integers[atIndex], atIndex, size
        ));

        int toCheck = Arrays.binarySearch(integers, shouldBeFound);
        System.out.println(format("Found by system method at an index: %d. Is equal: %b", 
            toCheck, toCheck == atIndex));
    }
}
```

The output file will print: 
> The model predicts: Java as the language of the file(files/test_file.txt).     
> Prediction procent: 0.997127347046451    

Confidence procentage: **99.71%**  

---
Or a simple approach to the algorithm in Java (binary search)

```java
/* This is a test file  */
/* Write code in here and let the model predict it */

class BinarySearch { 
    // Returns index of x if it is present in arr[l.. 
    // r], else return -1 
    int binarySearch(int arr[], int l, int r, int x) 
    { 
        if (r >= l) { 
            int mid = l + (r - l) / 2; 
  
            // If the element is present at the 
            // middle itself 
            if (arr[mid] == x) 
                return mid; 
  
            // If element is smaller than mid, then 
            // it can only be present in left subarray 
            if (arr[mid] > x) 
                return binarySearch(arr, l, mid - 1, x); 
  
            // Else the element can only be present 
            // in right subarray 
            return binarySearch(arr, mid + 1, r, x); 
        } 
  
        // We reach here when element is not present 
        // in array 
        return -1; 
    } 
  
    // Driver method to test above 
    public static void main(String args[]) 
    { 
        BinarySearch ob = new BinarySearch(); 
        int arr[] = { 2, 3, 4, 10, 40 }; 
        int n = arr.length; 
        int x = 10; 
        int result = ob.binarySearch(arr, 0, n - 1, x); 
        if (result == -1) 
            System.out.println("Element not present"); 
        else
            System.out.println("Element found at index " + result); 
    } 
} 

```

The output file will print:  
> The model predicts: Java as the language of the file(files/test_file.txt).   
> Prediction procent: 0.9958684418727  

Confidence procentage: **99.58%**  

## License
This project is licensed unde **MIT** [https://opensource.org/licenses/MIT/]

