#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
void main() {
	signed int num,num2,counter = 0;
	printf("Enter a number: ");
	scanf("%i", &num);
	num2 = num;
	while (num != 0) {
		if ((num & 1) == 1) {
			counter++;
		}
		num = num >> 1;
	}
	printf("The bit count of %i is %d", num2, counter);
}
