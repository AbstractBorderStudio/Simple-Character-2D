/***********************
 * Simple box material *
 ***********************
*/

shader_type canvas_item;

uniform vec4 color : source_color = vec4(1.0); 
uniform float bend_int = 0.0;

void vertex() {
	float y_int = 1.0 - UV.y;
	VERTEX.x += bend_int * -y_int;
}

void fragment() {
	COLOR = color;
}
