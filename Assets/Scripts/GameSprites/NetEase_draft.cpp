#include <iostream>
#include <cstdio>
#include <vector>
#include <math.h>       /* pow */

using namespace std;

int min(int a, int b) {
    return a > b ? b : a;
}

int max(int a, int b) {
    return a < b ? b : a;
}

int findBonus(const std::vector<std::vector<int>> board, std::vector<std::vector<int>> dist_matrix, int M, int x, int y, int L, int prev_L){
    if (L >= M) return 0;

    int A, B, C, D;
    // int E, F, G, H;
    A = max(x - L, 0);
    B = max(y - L, 0);
    C = min(x + L, M - 1);
    D = min(y + L, M - 1);

    int numBonus = 0;
    for (int i = A; i <= C; i++) {
        for (int j = B; j <= D; j++) {
            if (dist_matrix[i][j] < 0) 
                dist_matrix[i][j] = pow(pow(x - i, 2)+ pow(y - j,2),0.5);
            if (dist_matrix[i][j] > prev_L && dist_matrix[i][j] <= L) {
                numBonus += board[i][j]; 
            }
        }
    }
    return numBonus;
}


int main(){
    //freopen("1.in","r",stdin);
    int T,M,L,x,y;
    cin >> T;

    int k = 0;
    while (k < T) {
        cin >> M >> L;
        std::vector<std::vector<int>> board (M,std::vector<int>(M, 0));
        std::vector<std::vector<int>> dist_matrix(M,std::vector<int>(M, -1));
        // Store input
        for(int i = 0; i < M; i++){
            for(int j = 0; j < M; j++){
                int x; scanf("%d",&x);
                board[i][j] = x;
            }
        }
        cin >> x >> y;

        // Compute output
        int k = 0;
        int prev_L = 501; //larger than the limit of L
        do {
            L += k;
            k = findBonus(board, dist_matrix, M, x, y, L, prev_L);
            prev_L = L;
        } while (L < M && k != 0); //todo
        cout << L << endl;

        // To the next board
        k++;
    }
    return 0;
}


   // E, A, G, D-1
    if (x - r1 <)

    // A+1, F; G, C
    // B, F; C+1, H
    // E, B-1; D, H

    E = x - r2;
    F = x + r2;
    G = y - r2;
    H = y + r2;

        int r1, r2;
        r1 = 0; r2 = L;
        do {
            L += findBonus(board, M, x, y, r1, r2);
            r1 = r2 + 1;
            r2 = L;
        } while (r1 < M); //todo
        cout << L << endl;
