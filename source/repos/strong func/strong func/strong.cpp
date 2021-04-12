#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>

int counter = 0;

double recEffiPow(double base, int exponet) {
	counter++;
	printf("enter a base: ");
	scanf("%f",&base);
	printf("enter an exponet: ");
	scanf("%d",&exponet);
	int result = 1;
	for (exponet; exponet > 0; exponet--) {
		result = result * base;
	}
	printf("result: %d", result);
	printf("%d", counter);
	return 0;
}