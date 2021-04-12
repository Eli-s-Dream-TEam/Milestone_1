#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include "element.h"

void printElement(Element e) {
	printf("%c", e.c);
}

void scanElement(Element* e) {
	scanf(" %c", &e->c);
}
