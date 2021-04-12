#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
void printArr(int arr[],int size) {
	for (int i = size - 1; i >= 0; i--) {
		printf("%d ", arr[i]);
	}
}
void main() {
	int arr[] = { 34,56,54,32,67,89,90,32,21 };
	printArr(arr,9);
}