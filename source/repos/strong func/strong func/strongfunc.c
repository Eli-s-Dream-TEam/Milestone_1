#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
	int counter = 0;
	double recEffiPow(double base, int exponet) {
		printf("enter a base: ");
		scanf("%lf", &base);
		printf("enter an exponet: ");
		scanf("%d", &exponet);
		int result = 1;
		for (exponet; exponet > 0; exponet--) {
			counter++;
			result = result * base;
		}
		printf("count = %d", counter);
		return 0;
	}
