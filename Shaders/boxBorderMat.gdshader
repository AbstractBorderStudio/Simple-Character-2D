shader_type canvas_item;

uniform vec4 color : source_color = vec4(1.0); 
uniform float size = 0.1;

void fragment() {
	if (UV.x < 1.0 - size && UV.x > size && UV.y < 1.0 - size && UV.y > size)
	{
		COLOR = vec4(0.0);
	}
	else
	{
		COLOR = color;
	}
}
