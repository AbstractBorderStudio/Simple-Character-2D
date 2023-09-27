shader_type canvas_item;

uniform float innerRad = 0.4;
uniform float outerRad = 0.5;

uniform vec4 _inColor : source_color = vec4(1.0);
uniform vec4 _outColor : source_color = vec4(1.0);

void fragment() {
	vec2 uv = UV.xy - 1.0 / 2.0;
	
	if (length(uv) <= innerRad)
	{
		COLOR = _inColor;
	}
	else if (length(uv) > innerRad && length(uv) <= outerRad)
	{
		COLOR = _outColor;
	}
	else
	{
		COLOR = vec4(0.0);
	}
}
