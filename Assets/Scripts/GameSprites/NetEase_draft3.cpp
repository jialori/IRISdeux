#include <iostream>
#include <cstdio>
#include <vector>

using namespace std;

int abs(int a, int b) {
    return a - b >= 0 ? a - b : b - a;
}

int main(){
    int n;
    cin >> T;
	std::vector<int> A, V;
   // Store input
    for(int i = 0; i < n; i++){
        int x; scanf("%d",&x);
        A[i] = x;
    }
    for(int i = 0; i < n; i++){
        int x; scanf("%d",&x);
        V[i] = x;
    }
    
    for (int i)

    // 没那么简单，多组换数会达成更小，试试DP
    for(int i = 0; i < n; i++){
    	int k = A[i];
    	for(int j = 0; j <= i; j++) {
    		int val = V[j] * (abs(A[j], k);
    		if ())
    	}
    }

    return 0;
}