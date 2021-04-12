#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>

int nana(int a, int b) {
	return (a ^ b == 0);
}

int main() {
	int a = 7, b = 3;
	nana(a, b);
	return 0;
}