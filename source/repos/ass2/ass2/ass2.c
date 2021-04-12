void draw() {
	int i, j, n;
	printf("Enter a number:\n");
	scanf("%d", &n);
	if (n == 0)
		printf("X\n");
	else {
	n = 2 * n + 1;
		for (int i = 1;i <= n;i++)
		{
			for (j = 1;j <= n;j++)
			{
				if (i == 1 && j == 1 || i == 1 && j == n || i == n && j == 1 || i == n && j == n)
				{
					printf("+");
				}

				else if (i == 1 && j != 1 && j != n || i == n && j != 1 && j != n)
				{
					printf("-");
				}

				else if (j == 1 && i != 1 && i != n || j == n && i != 1 && i != n)
				{
					printf("|");
				}

				else if (i == j && i != 1 && i != n && j != 1 && j != n && i != n / 2 + 1)
				{
					printf("\\");
				}

				else if (i == n - j + 1 && i != n / 2 + 1)
				{
					printf("/");
				}
				else if (i == j && i == (n + 1) / 2)
				{
					printf("X");
				}
				else
				{
					printf(" ");
				}
			}
			printf("\n");
		}
	}

}

void isEvenLength() {
	char txt, counter = 0;
	printf("Enter text:\n");
	scanf(" %c", &txt);
	while (txt != '\n')
	{
		scanf("%c", &txt);
		counter++;
	}
	while (counter > 1)
	{
		counter -= 2;
	}
	if (counter > 0)
	{
		printf("your text's length is odd\n");
	}
	else
	{
		printf("your text's length is even\n");
	}

}

void identifyText() {
	char first, second;
	int in = 0, dc = 0, invalid = 0;
	printf("Enter text:\n");
	scanf(" %c", &first);
	if (first >= 'a' && first <= 'z')
		scanf(" %c", &second);
	while (second != '\n')
	{
		if (second >= 'a' && second <= 'z') {
			if (first > second) {
				dc = 1;
			}
			else if (first < second) {
				in = 1;
			}
		}
		else {
			invalid = 1;
		}
		first = second;
		scanf("%c", &second);
	}

	if (invalid == 0) {
		if (in == 1 && dc == 1) {
			printf("your text is mixed\n");
		}
		else if (in == 1 && dc == 0) {
			printf("your text is increasing\n");
		}
		else if (in == 0 && dc == 1) {
			printf("your text is decreasing\n");
		}
		else if (in == 0 && dc == 0) {
			printf("your text is constant\n");
		}
	}
	else {
		printf("your text is invalid\n");
	}

}

void hexToDec() {
	char hexnum;
	int digit, counter = 0, power = 1, num = 0,error=0;

	printf("Enter a reversed number in base 16:\n");
	scanf(" %c", &hexnum);
	while (hexnum != '\n')
	{
		digit = hexnum;
		if (digit >= '0' && digit <= '9') {
			digit = digit - '0';

		}
		else if (digit >= 'A' && digit <= 'F') {
			digit = digit - 'A' + 10;

		}
		else if (digit >= 'a' && digit <= 'f') {
			digit = digit - 'a' + 10;

		}
		else {
			printf("Error! %c is not a valid digit in base 16\n", digit);
			error = 1;
		}
		num = digit * power + num;
		power = power * 16;
		scanf("%c", &hexnum);
	}
	if (error == 0) {
		printf("%d\n", num);
	}

}

void baseToDec() {
	int base, digit, num2 = 0, power = 1, error = 0;
	char num1;
	printf("enter a base(2-10):\n");
	scanf("%d", &base);
	printf("Enter a reversed number in base %d:\n", base);
	scanf(" %c", &num1);
	while (num1 != '\n') {
		digit = num1 - '0';
		if ((digit < base) && (digit >= 0)) {
			num2 = digit * power + num2;
			power = power * base;
		}
		else {
			printf("Error! %d is not a valid digit in base %d\n", digit, base);
			error = 1;
		}
		scanf("%c", &num1);
	}
	if (error == 0) {
		printf("%d\n", num2);
	}
}

void bitCount() {
	int num,num2,counter = 0,rmdr;
	printf("Enter a number\n");
	scanf(" %d", &num);
	num2 = num;
	while (num != 0) {
		rmdr = num % 2;
		if (rmdr == 1) {
			counter++;
			}
		num = num / 2;
		}
	printf("The bit count of %d is %d\n", num2, counter);
}

#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
void main() {
	int choice;
	do {
		printf("Choose an option:\n1: Draw\n2: Even or Odd\n3: Text type\
                \n4: Hex to Dec\n5: Base to Dec\n6: Count bits\n0: Exit\n");
		scanf("%d", &choice);
		switch (choice) {
		case 1: draw();
			break;
		case 2: isEvenLength();
			break;
		case 3: identifyText();
			break;
		case 4: hexToDec();
			break;
		case 5: baseToDec();
			break;
		case 6: bitCount();
		case 0: break;
		default: printf("Wrong option!\n");
		}
	} while (choice != 0);
}