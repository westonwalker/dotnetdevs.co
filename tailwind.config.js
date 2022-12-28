/** @type {import('tailwindcss').Config} */
module.exports = {
	purge: {
		enabled: true,
		content: [
			'./Pages/**/*.cshtml',
			'./Pages/**/*.razor',
			'./Areas/**/*.cshtml',
			'./Views/**/*.chstml',
			'./Shared/**/*.razor'
		]
	},
	content: [],
	theme: {
		extend: {},
	},
	plugins: [
		require('@tailwindcss/forms'),
		require('@tailwindcss/line-clamp'),
	],
}
